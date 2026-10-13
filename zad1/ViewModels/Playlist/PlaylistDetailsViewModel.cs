namespace zad1.ViewModels.Playlist
{
    public class PlaylistDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> SongsName { get; set; } = new List<string>();
    }
}
