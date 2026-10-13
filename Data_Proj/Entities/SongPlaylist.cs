using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Proj.Entities
{
    public class SongPlaylist
    {
        public int SongId { get; set; }
        public Song Song { get; set; }

        // Връзка към плейлистата
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
    }
}
