using Data_Proj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Interfaces
{
    public interface ISongService
    {
        Task<List<Song>> GetAllSongAsync();
        Task<Song?> GetSongByIdAsync(int id);
        Task<Song?> GeSongDetailAsync(int id);
        Task AddSongAsync(Song song);
        Task UpdateSongAsync(Song song);
        Task DeleteSongAsync(int id);
    }
}
