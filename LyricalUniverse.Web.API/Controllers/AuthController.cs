using LyricalUniverse.Manager.Users.Interface;
using LyricalUniverse.Web.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserManager _userManager;
        private IConfiguration _configuration;

        public AuthController(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;

        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> GetToken(UserAuthModel model)
        {
            var user = await _userManager.Authenticate(model.UserName, model.Password);
            if (user == null)
                return BadRequest();

            var claims = new List<Claim>{
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("Id", user.Id.ToString()),
                    new Claim("Name", user.UserName),
                    new Claim("Email", user.Email),
                   };
            foreach (var item in user.UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Role.Name));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
            string loginToken = new JwtSecurityTokenHandler().WriteToken(token);        
            return Ok(loginToken);
        }
    }
}
