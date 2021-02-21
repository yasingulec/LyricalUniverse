using LyricalUniverse.Data.Repository.Users.Interface;
using LyricalUniverse.Entities;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> Authenticate(string userName, string password)
        {
            var user =await _ctx.Users.Include(x=>x.UserRoles).ThenInclude(x => x.Role).FirstOrDefaultAsync(x => x.UserName == userName && x.Password == password);
            return user;
        }
    }
}
