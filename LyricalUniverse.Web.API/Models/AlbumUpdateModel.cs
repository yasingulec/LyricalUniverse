using LyricalUniverse.Web.API.FileHelper.Validations;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Models
{
    public class AlbumUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime ReleaseDate { get; set; }
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".jpg", ".png", ".jpeg", ".gif" })]
        [MaxFileSize(5 * 1024 * 1024)]
        public IFormFile Image { get; set; }
    }
}
