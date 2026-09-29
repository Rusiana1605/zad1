using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace zad1.ViewModels.Song
{
    public class SongEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Range(1900, 2100)]
        public int ReleaseYear { get; set; }
        public int ArtistId { get; set; }
        public IEnumerable<SelectListItem>? Artists { get; set; }
    }
}
