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
    public class ArtistService : IArtistSevice
    {
        private readonly MusicDbContext context;

        public ArtistService(MusicDbContext context)
        {
            this.context = context;
        }
        public async Task AddArtistAsync(Artist artist)
        {
            context.Artists.Add(artist);
            await context.SaveChangesAsync();
        }
        public async Task DeleteArtistAsync(int id)
        {
            var hospital = await context.Artists.FindAsync(id);
            if (hospital != null)
            {
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Artist>> GetAllArtistsAsync()
        {
            return await context.Artists.ToListAsync();
        }
        public async Task<Artist?> GetArtistByIdAsync(int id)
        {
            return await context.Artists.FindAsync(id);
        }

        public async Task<Artist?> GetArtistDetailAsync(int id)
        {
            return await context.Artists
               .Include(x => x.Songs)
               .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task UpdateArtistAsync(Artist artist)
        {
            await context.SaveChangesAsync();
        }
    }
}
