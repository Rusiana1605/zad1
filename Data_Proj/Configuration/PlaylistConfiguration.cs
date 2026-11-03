using Data_Proj.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Proj.Configuration
{
    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.HasData(
                new Playlist
                {
                    Id = 1,
                    Title = "METAAAAAAL",
                    Description = "Metal music playlist"
                },
                new Playlist
                {
                    Id = 2,
                    Title = "Rocky",
                    Description = "Rock music hits playlist"
                },
                 new Playlist
                 {
                     Id = 3,
                     Title = "BG kupon",
                     Description = "Bulgarska muzika"
                 }
                );
        }
    }
}
