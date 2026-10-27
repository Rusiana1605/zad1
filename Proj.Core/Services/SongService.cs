using Data_Proj;
using Data_Proj.Entities;
using Microsoft.EntityFrameworkCore;
using Proj.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Services
{
    public class SongService : ISongService
    {
        private readonly MusicDbContext context;

        public SongService(MusicDbContext context)
        {
            this.context = context;
        }
        public async Task AddSongAsync(Song song)
        {
            context.Songs.Add(song);
            await context.SaveChangesAsync();
        }

        public async Task DeleteSongAsync(int id)
        {
            var doctor = await context.Songs
                .Include(x => x.SongPlaylists)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (doctor != null)
            {
                context.SongPlaylists.RemoveRange(doctor.SongPlaylists);
                context.Songs.Remove(doctor);
                await context.SaveChangesAsync();
            }
        }

        public async Task<Song?> GeSongDetailAsync(int id)
        {
            return await context.Songs
            .Include(x => x.SongPlaylists)
            .ThenInclude(x => x.Playlist)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Song>> GetAllSongAsync()
        {
            return await context.Songs
                  .Include(x => x.SongPlaylists)
                  .ThenInclude(x => x.Playlist)
                  .ToListAsync();
        }

        public async Task<Song?> GetSongByIdAsync(int id)
        {
            return await context.Songs.FindAsync(id);
        }

        public async Task UpdateSongAsync(Song song)
        {
            await context.SaveChangesAsync();
        }
    }
}
