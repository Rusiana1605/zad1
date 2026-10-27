using Data_Proj;
using Data_Proj.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj.Core.Interfaces;
using zad1.ViewModels.Playlist;

namespace zad1.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly IPlaylistService playserv;
        private readonly ISongService songserv;
        public PlaylistController(IPlaylistService playserv, ISongService songserv)
        {
            this.playserv = playserv;
            this.songserv = songserv;
        }
        public async Task<IActionResult> Index()
        {
            var patients = await playserv.GetAllPlaylistsAsync();
            var model = new List<PlaylistIndexViewModel>();
            foreach (var patient in patients)
            {
                model.Add(new PlaylistIndexViewModel
                {
                    Id = patient.Id,
                    Title = patient.Title,
                    Description = patient.Description,
                    SongsCount = patient.SongPlaylists.Select(dp => dp.SongId).Count()
                });
            }
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            await LoadSongs();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(PlaylistCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var playlist = new Playlist
                {
                    Title = model.Title,
                    Description = model.Description,
                };
                foreach (var songId in model.SongsIds)
                {
                    playlist.SongPlaylists.Add(new SongPlaylist
                    {
                        SongId = songId,
                    });
                }
               await playserv.AddPlaylistAsync(playlist);
                return RedirectToAction(nameof(Index));
            }
            await LoadSongs(model.SongsIds);
            return View(model);
        }
        public async Task LoadSongs(List<int> selectedSongs = null) // Helper method to load doctors for the multi-select list.  
        {
            var doctors = await songserv.GetAllSongAsync();
            var dd = doctors.Select(d => new
            {
                d.Id,
                Name = d.Title + " " + d.ReleaseYear
            }).ToList();
            ViewBag.Songs = new MultiSelectList(doctors, "Id", "Name", selectedSongs);
        }
        public async Task<IActionResult> Details(int id)
        {
            var patient = await playserv.GetPlaylistWithSongsAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var model = new PlaylistDetailsViewModel
            {
                Id = patient.Id,
                Title = patient.Title,
                Description = patient.Description,
                SongsName = patient.SongPlaylists.Select(dp => dp.Song.Title).ToList()
            };
            return View(model);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await playserv.GetPlaylistWithSongsAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            var model = new PlaylistEditViewModel
            {
                Id = patient.Id,
                Title = patient.Title,
                Description = patient.Description,
                SongIds = patient.SongPlaylists.Select(dp => dp.SongId).ToList()
            };
            await LoadSongs(model.SongIds);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlaylistEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var patient = await playserv.GetPlaylistWithSongsAsync(id);
                if (patient == null)
                {
                    return NotFound();
                }
                patient.Title = model.Title;
                patient.Description = model.Description;
                // Update doctor associations
                patient.SongPlaylists.Clear();
                foreach (var doctorId in model.SongIds)
                {
                    patient.SongPlaylists.Add(new SongPlaylist
                    {
                        SongId = doctorId,
                        PlaylistId = patient.Id
                    });
                }
                await playserv.UpdatePlaylistAsync(patient, model.SongIds);
                return RedirectToAction(nameof(Index));
            }
            await LoadSongs(model.SongIds);
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await playserv.GetPlaylistByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            await playserv.DeletePlaylistAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await playserv.GetPlaylistByIdAsync(id);
            if (patient != null)
            {
                await playserv.DeletePlaylistAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
