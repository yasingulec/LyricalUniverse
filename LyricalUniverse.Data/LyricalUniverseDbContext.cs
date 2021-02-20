using LyricalUniverse.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Data
{
    public class LyricalUniverseDbContext : DbContext
    {
        public LyricalUniverseDbContext(DbContextOptions<LyricalUniverseDbContext> options) : base(options)
        {

        }

        public DbSet<Album> Albums { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
