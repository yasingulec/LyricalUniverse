using LyricalUniverse.Core;
using LyricalUniverse.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntity, new()
    {
        private LyricalUniverseDbContext _ctx;
        public GenericRepository(LyricalUniverseDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task AddAsync(T entity)
        {
            await _ctx.Set<T>().AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _ctx.Set<T>().Remove(entity);
            await SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync<T>();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _ctx.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            bool isSaved = await _ctx.SaveChangesAsync() > 0 ? true : false;
            return isSaved;
        }

        public async Task UpdateAsync(T entity)
        {
            _ctx.Set<T>().Update(entity);
            await SaveChangesAsync();
        }
    }
}
