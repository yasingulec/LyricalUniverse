using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Tracks.Interface;
using LyricalUniverse.Web.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TrackController : ControllerBase
    {
        private ITrackManager _trackManager;
        public TrackController(ITrackManager trackManager)
        {
            _trackManager = trackManager;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTracks()
        {
            var tracks = await _trackManager.GetAllAsync();
            return Ok(tracks);
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTrack(int id)
        {
            var track = await _trackManager.GetAsync(id);
            return Ok(track);
        }
        [HttpPost]
        public async Task<IActionResult> AddTrack(Track model)
        {
            if (model.Id == 0)
            {
                await _trackManager.AddAsync(model);
                return Ok();
            }
            else
                return BadRequest();

        }
        //[HttpPost]
        //public async Task<IActionResult> UpdateTrack()
        //{

        //}
        [HttpGet]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            await _trackManager.DeleteAsync(id);
            return Ok();
        }
    }
}
