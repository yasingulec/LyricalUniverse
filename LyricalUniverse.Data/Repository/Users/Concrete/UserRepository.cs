using LyricalUniverse.Data.Repository.Users.Interface;
using LyricalUniverse.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Data.Repository.Users.Concrete
{
    public class UserRepository:GenericRepository<User>,IUserRepository
    {
        private LyricalUniverseDbContext _ctx;
        public UserRepository(LyricalUniverseDbContext ctx):base(ctx)
        {
            _ctx = ctx;
        }
    }
}
