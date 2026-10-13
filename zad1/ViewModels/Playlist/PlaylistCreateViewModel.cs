using System.ComponentModel.DataAnnotations;

namespace zad1.ViewModels.Playlist
{
    public class PlaylistCreateViewModel
    {
        [Required(ErrorMessage = "Заглавието е задължително!")]
        [StringLength(100, ErrorMessage = "Заглавието трябва да е до 100 символа.")]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        public List<int> SongsIds { get; set; } = new List<int>();
    }
}
