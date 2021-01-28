using AutoMapper;
using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Albums.Interface;
using LyricalUniverse.Web.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace LyricalUniverse.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private IAlbumManager _albumManager;
        private readonly IMapper _mapper;
        public AlbumController(IAlbumManager albumManager, IMapper mapper)
        {
            _albumManager = albumManager;
            _mapper = mapper;
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
        [HttpPost]
        public async Task<IActionResult> AddAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                await _albumManager.AddAsync(album);
                return Ok(album);
            }
            return BadRequest();
        }
    }
}
