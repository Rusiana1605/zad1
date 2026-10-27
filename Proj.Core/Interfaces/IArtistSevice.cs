using Data_Proj.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj.Core.Interfaces
{
    public interface IArtistSevice
    {
        Task<List<Artist>> GetAllArtistsAsync();
        Task<Artist?> GetArtistByIdAsync(int id);
        Task<Artist?> GetArtistDetailAsync(int id);
        Task AddArtistAsync(Artist artist);
        Task UpdateArtistAsync(Artist artist);
        Task DeleteArtistAsync(int id);
    }
}
