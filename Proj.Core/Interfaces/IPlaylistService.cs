using Data_Proj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Interfaces
{
    public interface IPlaylistService
    {
        Task<List<Playlist>> GetAllPlaylistsAsync();
        Task<Playlist?> GetPlaylistByIdAsync(int id);
        Task<Playlist?> GetPlaylistWithSongsAsync(int id);
        Task AddPlaylistAsync(Playlist playlist);
        Task UpdatePlaylistAsync(Playlist playlist, List<int> songsIds);
        Task DeletePlaylistAsync(int id);
    }
}
