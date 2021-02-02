using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LyricalUniverse.Web.API.Models
{
    public class AlbumCreateModel
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile Image { get; set; } = null;
    }
}
