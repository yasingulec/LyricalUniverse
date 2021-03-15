using LyricalUniverse.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Entities
{
    public class Track:BaseEntity
    {
        public int TrackId { get; set; }
        public string Lyric { get; set; }
        public Album Album { get; set; }
    }
}
