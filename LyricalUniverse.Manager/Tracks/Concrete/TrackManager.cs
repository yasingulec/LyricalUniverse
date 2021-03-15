using LyricalUniverse.Data.Repository.Tracks.Interface;
using LyricalUniverse.Entities;
using LyricalUniverse.Manager.Tracks.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyricalUniverse.Manager.Tracks.Concrete
{
    public class TrackManager : ITrackManager
    {
        private ITrackRepository _trackRepository;
        public TrackManager(ITrackRepository trackRepository)
        {
            _trackRepository = trackRepository;
        }
        public async Task AddAsync(Track entity)
        {
            await _trackRepository.AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _trackRepository.DeleteAsync(id);
        }

        public async Task<List<Track>> GetAllAsync()
        {
            var tracks = await _trackRepository.GetAllAsync();
            return tracks;
        }

        public async Task<Track> GetAsync(int id)
        {
            var track = await _trackRepository.GetAsync(id);
            return track;
        }

        public async Task UpdateAsync(Track entity)
        {
            await _trackRepository.UpdateAsync(entity);
        }
    }
}
