using System.ComponentModel.DataAnnotations;

namespace zad1.ViewModels.Playlist
{
    public class PlaylistEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Заглавието е задължително!")]
        [StringLength(100, ErrorMessage = "Заглавието трябва да е до 100 символа.")]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please enter doctor")]
        public List<int> SongIds { get; set; } = new List<int>();
    }
}
