using System.ComponentModel.DataAnnotations;

namespace zad1.ViewModels.Artist
{
    public class ArtistCreateViewModel
    {
            [Required]
            [StringLength(50)]
            public string Name { get; set; }

            [Required]
            [StringLength(50)]
            public string Genre { get; set; }

            [Range(1900, 2100)]
            public int FoundedYear { get; set; }
    }
}
