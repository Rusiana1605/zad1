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
    public class SongPlaylistConfiguration : IEntityTypeConfiguration<SongPlaylist>
    {
        public void Configure(EntityTypeBuilder<SongPlaylist> builder)
        {
            builder.HasData(
                new SongPlaylist { SongId = 1, PlaylistId = 3 },
                new SongPlaylist { SongId = 2, PlaylistId = 2 },
                new SongPlaylist { SongId = 3, PlaylistId = 1 }
                );
        }
    }
}
