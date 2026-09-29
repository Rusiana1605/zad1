using Data_Proj;
using Data_Proj.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zad1.ViewModels.Artist;

namespace zad1.Controllers
{
    public class ArtistController : Controller
    {
        private readonly MusicDbContext context;

        public ArtistController(MusicDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await context.Artists
                .Select(a => new ArtistIndexViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    Genre = a.Genre,
                    FoundedYear = a.FoundedYear
                })
                .ToListAsync();

            return View(artists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            ArtistCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var artist = new Artist
            {
                Name = model.Name,
                Genre = model.Genre,
                FoundedYear = model.FoundedYear
            };

            context.Artists.Add(artist);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var artist = await context.Artists
                .Include(a => a.Songs)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (artist == null)
            {
                return NotFound();
            }

            var model = new ArtistDetailsViewModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre,
                FoundedYear = artist.FoundedYear,
            };

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var artist = await context.Artists
                .FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            var model = new ArtistEditViewModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre,
                FoundedYear = artist.FoundedYear
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            ArtistEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var artist = await context.Artists
                .FindAsync(model.Id);

            if (artist == null)
            {
                return NotFound();
            }

            artist.Name = model.Name;
            artist.Genre = model.Genre;
            artist.FoundedYear = model.FoundedYear;

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var artist = await context.Artists
                .FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            var model = new ArtistDeleteViewModel
            {
                Id = artist.Id,
                Name = artist.Name,
                Genre = artist.Genre
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artist = await context.Artists
                .FindAsync(id);

            if (artist == null)
            {
                return NotFound();
            }

            context.Artists.Remove(artist);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
