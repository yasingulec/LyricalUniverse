using LyricalUniverse.Data.Albums.Interface;
using LyricalUniverse.Data.Repository;
using LyricalUniverse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Data.Albums.Concrete
{
    public class AlbumRepository:GenericRepository<Album>,IAlbumRepository
    {
        private LyricalUniverseDbContext _ctx;
        public AlbumRepository(LyricalUniverseDbContext ctx):base(ctx)
        {
            _ctx = ctx;
        }
    }
}
