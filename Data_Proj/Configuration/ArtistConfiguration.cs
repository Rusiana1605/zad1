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
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.HasData(
                new Artist
                {
                    Id = 1,
                    Name = "The Beatles",
                    Genre = "Rock",
                    FoundedYear = 1960
                },
                new Artist
                {
                    Id = 2,
                    Name = "Rammstein",
                    Genre ="Metal",
                    FoundedYear = 1994
                },
                new Artist
                {   
                    Id = 3,
                    Name = "Metallica",
                    Genre = "Metal",
                    FoundedYear = 1981
                });
        }
    }
}
