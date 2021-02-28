using LyricalUniverse.Web.API.FileHelper.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Models
{
    public class UserCreateUpdateModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string ImagePath { get; set; }
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".gif" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile Image { get; set; } = null;
    }
}
