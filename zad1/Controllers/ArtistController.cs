using Data_Proj.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proj.Core.Interfaces;
using Proj.Core.Services;
using zad1.ViewModels.Artist;

namespace zad1.Controllers
{
    public class ArtistController : Controller
    {
        private readonly IArtistSevice artistserv;

        public ArtistController(IArtistSevice artistserv)
        {
            this.artistserv = artistserv;
        }

        public async Task<IActionResult> Index()
        {
            var artists = await artistserv.GetAllArtistsAsync();
            var model = new List<ArtistIndexViewModel>();
            foreach (var artist in artists)
            {
                model.Add(new ArtistIndexViewModel
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    Genre = artist.Genre,
                    FoundedYear = artist.FoundedYear
                });
            }

            return View(artists);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ArtistCreateViewModel model)
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

            await artistserv.AddArtistAsync(artist);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var artist = await artistserv.GetArtistByIdAsync(id);

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
            var artist = await artistserv.GetArtistByIdAsync(id);

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
        public async Task<IActionResult> Edit(ArtistEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var artist = await artistserv.GetArtistByIdAsync(model.Id);

            if (artist == null)
            {
                return NotFound();
            }

            artist.Name = model.Name;
            artist.Genre = model.Genre;
            artist.FoundedYear = model.FoundedYear;

            await artistserv.UpdateArtistAsync(artist);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var artist = await artistserv.GetArtistByIdAsync(id);

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
            var artist = await artistserv.GetArtistByIdAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            await artistserv.DeleteArtistAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
