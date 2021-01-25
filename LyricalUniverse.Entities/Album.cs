using LyricalUniverse.Core;
using System;

namespace LyricalUniverse.Entities
{
    public class Album : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
