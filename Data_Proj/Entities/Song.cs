using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Proj.Entities
{
    public class Song
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }

        public int ArtistId { get; set; }

        public Artist Artist { get; set; }
    }
}
