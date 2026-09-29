using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace zad1.ViewModels.Song
{
    public class SongCreateViewModel
    {
        public int Id { get; internal set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100)]
        public string Title { get; set; }

        [Range(1900, 2100, ErrorMessage = "Enter valis year")]
        public int ReleaseYear { get; set; }
        [Required(ErrorMessage = "Choose an artist")]
        [Display(Name = "Artist")]
        public int ArtistId { get; set; }
        public IEnumerable<SelectListItem>? Artists { get; set; }

    }
}
