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
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasData(
                new Song()
                {
                    Id = 1,
                    Title = "Fade To Black",
                    ReleaseYear = 1984,
                    ArtistId = 3
                },
                new Song()
                {
                    Id = 2,
                    Title = "Stein um Stein",
                    ReleaseYear = 2004,
                    ArtistId = 2
                },
                new Song()
                {
                    Id = 3,
                    Title = "Hey,Jude",
                    ReleaseYear = 1968,
                    ArtistId = 1
                });
        }
    }
}
