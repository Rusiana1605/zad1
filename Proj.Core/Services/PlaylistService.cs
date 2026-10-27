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
    public class PlaylistService : IPlaylistService
    {
        private readonly MusicDbContext context;

        public PlaylistService(MusicDbContext context)
        {
            this.context = context;
        }
        public async Task AddPlaylistAsync(Playlist playlist)
        {
            context.Playlists.Add(playlist);
            await context.SaveChangesAsync();
        }

        public Task DeletePlaylistAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Playlist>> GetAllPlaylistsAsync()
        {
            return await context.Playlists
               .Include(x => x.SongPlaylists)
               .ThenInclude(x => x.Song)
               .ToListAsync();
        }

        public async Task<Playlist?> GetPlaylistByIdAsync(int id)
        {
            return await context.Playlists.FindAsync(id);
        }

        public async Task<Playlist?> GetPlaylistWithSongsAsync(int id)
        {
            return await context.Playlists
            .Include(x => x.SongPlaylists)
            .ThenInclude(x => x.Song)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdatePlaylistAsync(Playlist playlist, List<int> songsIds)
        {
            playlist.SongPlaylists.Clear();
            foreach (var songsids in songsIds.Distinct())
            {
                playlist.SongPlaylists.Add(new SongPlaylist
                {
                    SongId = songsids,
                    PlaylistId = playlist.Id
                });
            }
            await context.SaveChangesAsync();
        }
    }
}
