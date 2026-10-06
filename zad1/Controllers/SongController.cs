using Data_Proj;
using Data_Proj.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zad1.ViewModels.Artist;
using zad1.ViewModels.Song;

namespace zad1.Controllers
{
    public class SongController : Controller
    {
        private readonly MusicDbContext context;

        public SongController(MusicDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var songs = await context.Songs
             .Include(s => s.Artist) 
              .Select(s => new SongIndexViewModel
            {
             Id = s.Id,
             Title = s.Title,
             ReleaseYear = s.ReleaseYear,
             ArtistName = s.Artist.Name
            })
             .ToListAsync();
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

                context.Songs.Add(song);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Ако моделът не е валиден, зареждаме отново артистите за падащото меню
            model.Artists = await GetArtistsSelectListAsync();
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var song = await context.Songs
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (song == null) return NotFound();

            var songViewModel = new SongDetailsViewModel
            {
                Id = song.Id,
                Title = song.Title,
                ReleaseYear = song.ReleaseYear,
                ArtistName = song.Artist.Name,
                ArtistGenre = song.Artist.Genre,
                ArtistFoundedYear = song.Artist.FoundedYear
            };

            return View(songViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var song = await context.Songs.FindAsync(id);
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
                var song = await context.Songs.FindAsync(id);
                if (song == null) return NotFound();

                song.Title = model.Title;
                song.ReleaseYear = model.ReleaseYear;
                song.ArtistId = model.ArtistId;

                context.Update(song);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            model.Artists = await GetArtistsSelectListAsync();
            return View(model);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var song = await context.Songs
                .Include(s => s.Artist)
                .FirstOrDefaultAsync(m => m.Id == id);
            
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
            var song = await context.Songs.FindAsync(id);
            if (song != null)
            {
                context.Songs.Remove(song);
                await context.SaveChangesAsync();
            }
            
            return RedirectToAction(nameof(Index));
        }

        // Асинхронен помощен метод за извличане на списъка с артисти
        private async Task<IEnumerable<SelectListItem>> GetArtistsSelectListAsync()
        {
            return await context.Artists
                .Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name
                })
                .ToListAsync();
        }
    }
}
