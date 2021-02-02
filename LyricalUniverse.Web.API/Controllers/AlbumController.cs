using AutoMapper;
using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Albums.Interface;
using LyricalUniverse.Web.API.FileHelper;
using LyricalUniverse.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Controllers
{
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
        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await _albumManager.GetAllAsync();
            var albumModel = _mapper.Map<List<AlbumModel>>(albums);
            return Ok(albumModel);
        }
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
                //albumModel.ImagePath=_fileManager.SaveImage()
                await _albumManager.AddAsync(album);
                return CreatedAtAction("AddAlbum", album);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAlbum(Album albumModel)
        {
            if (albumModel.Id > 0)
            {
                var album = await _albumManager.GetAsync(albumModel.Id);
                _mapper.Map<Album, AlbumModel>(albumModel);
                await _albumManager.UpdateAsync(album);
                return Ok(album);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
