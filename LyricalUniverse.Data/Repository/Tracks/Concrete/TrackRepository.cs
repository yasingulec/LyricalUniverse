using LyricalUniverse.Data.Repository.Tracks.Interface;
using LyricalUniverse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Data.Repository.Tracks.Concrete
{
   public class TrackRepository:GenericRepository<Track>,ITrackRepository
    {
        private LyricalUniverseDbContext _ctx;
        public TrackRepository(LyricalUniverseDbContext ctx):base(ctx)
        {
            _ctx = ctx;
        }

    }
}
