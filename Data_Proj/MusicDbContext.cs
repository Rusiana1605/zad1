using Data_Proj.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Proj
{
    public class MusicDbContext:DbContext
    {
        public MusicDbContext(
            DbContextOptions<MusicDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<SongPlaylist> SongPlaylists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         
            modelBuilder.Entity<SongPlaylist>()
                .HasKey(sp => new { sp.SongId, sp.PlaylistId });

            modelBuilder.Entity<SongPlaylist>()
                .HasOne(sp => sp.Song)
                .WithMany() 
                .HasForeignKey(sp => sp.SongId);

            modelBuilder.Entity<SongPlaylist>()
                .HasOne(sp => sp.Playlist)
                .WithMany(p => p.SongPlaylists)
                .HasForeignKey(sp => sp.PlaylistId);
        }
    }
}
