using LyricalUniverse.Manager.Albums.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AlbumController : ControllerBase
    {
        private IAlbumManager _albumManager;
        public AlbumController(IAlbumManager albumManager)
        {
            _albumManager = albumManager;
        }
        [HttpGet]
        public async Task <IActionResult> GetAlbums()
        {
            var albums =await _albumManager.GetAllAsync();
            return Ok(albums);
        }
    }
}
