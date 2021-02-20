using LyricalUniverse.Data.Repository.Users.Interface;
using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Users.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Manager.Users.Concrete
{
    public class UserManager : IUserManager
    {
        private IUserRepository _repo;
        public UserManager(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task AddAsync(User entity)
        {
           await _repo.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();
            return users;
        }

        public async Task<User> GetAsync(int id)
        {
            var user = await _repo.GetAsync(id);
            return user;
        }

        public async Task UpdateAsync(User entity)
        {
          await  _repo.UpdateAsync(entity);
        }
    }
}
