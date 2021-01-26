using LyricalUniverse.Data.Albums.Interface;
using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Albums.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Manager.Albums.Concrete
{
    public class AlbumManager : IAlbumManager
    {
        private IAlbumRepository _albumRepository;
        public AlbumManager(IAlbumRepository albumRepository)
        {
            _albumRepository = albumRepository;
        }

        public async Task AddAsync(Album entity)
        {
           await _albumRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
           await _albumRepository.DeleteAsync(id);
        }

        public async Task<List<Album>> GetAllAsync()
        {
           return await _albumRepository.GetAllAsync();
        }

        public async Task<Album> GetAsync(int id)
        {
            return await _albumRepository.GetAsync(id);
        }

        public async Task UpdateAsync(Album entity)
        {
            await _albumRepository.UpdateAsync(entity);
        }
    }
}
