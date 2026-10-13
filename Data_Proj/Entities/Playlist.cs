using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Proj.Entities
{
    public class Playlist
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(300)]
        public string Description { get; set; }

        public ICollection<SongPlaylist> SongPlaylists { get; set; } = new List<SongPlaylist>();
    }
}
