using AutoMapper;
using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Albums.Interface;
using LyricalUniverse.Web.API.FileHelper;
using LyricalUniverse.Web.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private IAlbumManager _albumManager;
        private readonly IMapper _mapper;
        private IFileManager _fileManager;
        public AlbumController(IAlbumManager albumManager, IMapper mapper, IFileManager fileManager)
        {
            _albumManager = albumManager;
            _mapper = mapper;
            _fileManager = fileManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await _albumManager.GetAllAsync();
            var albumModel = _mapper.Map<List<AlbumModel>>(albums);
            return Ok(albumModel);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAlbum(int id)
        {
            var album = await _albumManager.GetAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            var albumModel = _mapper.Map<AlbumModel>(album);
            return Ok(albumModel);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAlbumByName(string name)
        {
            var album = await _albumManager.GetAlbumByNameAsync(name);
            if (album == null)
            {
                return NotFound();
            }
            var albumModel = _mapper.Map<AlbumModel>(album);
            return Ok(albumModel);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAlbum(int id)
        {
            if (id>0)
            {
                await _albumManager.DeleteAsync(id);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddAlbum([FromForm]AlbumCreateModel albumModel)
        {
            if (ModelState.IsValid)
            {
                var album = new Album
                {
                    ImagePath = _fileManager.SaveImage(albumModel.Image),
                    Description=albumModel.Description,
                    Name=albumModel.Name,
                    ReleaseDate=albumModel.ReleaseDate,
                };
                await _albumManager.AddAsync(album);
                return CreatedAtAction("AddAlbum", album);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAlbum([FromForm]AlbumUpdateModel albumModel)
        {
            if (albumModel.Id > 0)
            {
                var album = await _albumManager.GetAsync(albumModel.Id);


                if (String.IsNullOrEmpty(album.ImagePath) && albumModel.Image != null)
                {
                    album.ImagePath = _fileManager.SaveImage(albumModel.Image);
                }

                if (albumModel.Image==null && album.ImagePath != null)               
                    albumModel.ImagePath = album.ImagePath;                
                
                if (album.ImagePath != albumModel.ImagePath)
                {
                    _fileManager.RemoveImage(album.ImagePath);
                    album.ImagePath = _fileManager.SaveImage(albumModel.Image);
                }

                album.Name = albumModel.Name;
                album.Description = albumModel.Description;
                album.ReleaseDate = albumModel.ReleaseDate;
                await _albumManager.UpdateAsync(album);
                return Ok(album);
            }
            else
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpGet("{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf(".") + 1);
            return new FileStreamResult(_fileManager.imageStream(image), $"image/{mime}");
        }
    }
}
