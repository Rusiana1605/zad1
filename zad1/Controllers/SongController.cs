using Data_Proj;
using Data_Proj.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proj.Core.Interfaces;
using System.Numerics;
using zad1.ViewModels.Artist;
using zad1.ViewModels.Song;

namespace zad1.Controllers
{
    public class SongController : Controller
    {
        private readonly ISongService songserv;
        private readonly IArtistSevice artistserv;

        public SongController(ISongService songserv, IArtistSevice artistserv)
        {
            this.songserv = songserv;
            this.artistserv = artistserv;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await songserv.GetAllSongAsync();
            var model = new List<SongIndexViewModel>();
            foreach (var song in songs)
            {
                model.Add(new SongIndexViewModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    ReleaseYear = song.ReleaseYear,
                    ArtistName = song.Artist.Name
                });
            }
            return View(songs);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new SongCreateViewModel
            {
                Artists = await GetArtistsSelectListAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SongCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var song = new Song
                {
                    Title = model.Title,
                    ReleaseYear = model.ReleaseYear,
                    ArtistId = model.ArtistId
                };

               await songserv.AddSongAsync(song);
                return RedirectToAction(nameof(Index));
            }

            // Ако моделът не е валиден, зареждаме отново артистите за падащото меню
            model.Artists = await GetArtistsSelectListAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null) return NotFound();

            var song = await songserv.GeSongDetailAsync(id);

            if (song == null) return NotFound();

            var songViewModel = new SongDetailsViewModel
            {
                Id = song.Id,
                Title = song.Title,
                ReleaseYear = song.ReleaseYear,
                ArtistName = song.Artist.Name,
                ArtistGenre = song.Artist.Genre,
                ArtistFoundedYear = song.Artist.FoundedYear,
                Playlists = song.SongPlaylists
                .Select(dp => $"{dp.Playlist.Id} {dp.Playlist.Title}").ToList()
            };
            return View(songViewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null) return NotFound();

            var song = await songserv.GetSongByIdAsync(id);
            if (song == null) return NotFound();

            var viewModel = new SongEditViewModel
            {
                Id = song.Id, // Уверете се, че SongCreateViewModel има Id
                Title = song.Title,
                ReleaseYear = song.ReleaseYear,
                ArtistId = song.ArtistId,
                Artists = await GetArtistsSelectListAsync()
            };


            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SongEditViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var song = await songserv.GetSongByIdAsync(id);
                if (song == null) return NotFound();

                song.Title = model.Title;
                song.ReleaseYear = model.ReleaseYear;
                song.ArtistId = model.ArtistId;

                await songserv.UpdateSongAsync(song);
                return RedirectToAction(nameof(Index));
            }

            model.Artists = await GetArtistsSelectListAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return NotFound();

            var song = await songserv.GetSongByIdAsync(id);
            
            if (song == null) return NotFound();

            var songViewModel = new SongDeleteViewModel
            {
                Id = song.Id,
                Title = song.Title,
            };

            return View(songViewModel);
        }

        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await songserv.GetSongByIdAsync(id);
            if (song != null)
            {
                await songserv.DeleteSongAsync(id);
            }
            
            return RedirectToAction(nameof(Index));
        }

        // Асинхронен помощен метод за извличане на списъка с артисти
        private async Task<IEnumerable<SelectListItem>> GetArtistsSelectListAsync()
        {
            var artists = await this.artistserv.GetAllArtistsAsync();

            return artists.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            })
            .ToList();
        }
        
    }
}
