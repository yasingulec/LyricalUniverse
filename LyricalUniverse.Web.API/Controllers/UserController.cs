using AutoMapper;
using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Users.Interface;
using LyricalUniverse.Web.API.FileHelper.UserFileManager;
using LyricalUniverse.Web.API.Models;
using LyricalUniverse.Web.API.Models.Common;
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
    public class UserController : ControllerBase
    {
        private IUserManager _userManager;
        private readonly IMapper _mapper;
        private IUserFileManager _fileManager;
        public UserController(IUserManager userManager, IMapper mapper, IUserFileManager fileManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _fileManager = fileManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = new Response<List<UserViewModel>>();
            var users = await _userManager.GetAllAsync();
            var userModel = _mapper.Map<List<UserViewModel>>(users);
            response.Data = userModel;
            response.isSuccess = true;
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> GetUser(UserViewModel uvm)
        {
            var response = new Response<UserViewModel>();
            var user = await _userManager.GetAsync(uvm.Id);
            if (user == null)
                return NotFound();
            try
            {
                var userModel = _mapper.Map<UserViewModel>(user);
                response.Data = userModel;
                response.isSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = ($"ex: - {ex}");
                response.isSuccess = false;
            }
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromForm] UserCreateUpdateModel ucm)
        {
            var response = new Response<UserCreateUpdateModel>();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new User
                    {
                        ImagePath = _fileManager.SaveImage(ucm.Image),
                        Email = ucm.Email,
                        UserName = ucm.UserName,
                        Password = ucm.Password
                    };
                    await _userManager.AddAsync(user);
                    response.isSuccess = true;
                    response.Message = "Kayıt başarılı bir şekilde eklendi.";

                }
                catch (Exception ex)
                {

                    response.isSuccess = false;
                    response.Message = $"ex - {ex}";
                    return BadRequest(response);
                }
            }
            return Ok(response);
        }
    }
}
