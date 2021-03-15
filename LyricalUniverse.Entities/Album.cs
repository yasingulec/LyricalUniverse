using LyricalUniverse.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LyricalUniverse.Entities
{
    public class Album : BaseEntity
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage ="Bu alan boş geçilemez.")]
        public DateTime ReleaseDate { get; set; }
        public List<Track> Track { get; set; }
    }
}
