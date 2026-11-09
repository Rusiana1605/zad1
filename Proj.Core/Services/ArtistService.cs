using Data_Proj;
using Data_Proj.Entities;
using Microsoft.EntityFrameworkCore;
using Proj.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
                context.Artists.Remove(hospital);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Artist>> GetAllArtistsAsync(
            string? search,
            string? genre,
            string? sortOrder)
        {
            IQueryable<Artist> query = context.Artists;
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(t => t.Name.Contains(search));
            }
            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(t => t.Genre != null && t.Genre.Contains(genre));
            }
            switch (sortOrder)
            {
                case "name":
                    query = query.OrderBy(h => h.Name);
                    break;
                case "genre":
                    query = query.OrderBy(h => h.Genre);
                    break;
                default:
                    query = query.OrderBy(h => h.Name);
                    break;
            }
            return await query.ToListAsync();
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
