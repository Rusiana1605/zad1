using Data_Proj;
using Data_Proj.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using zad1.ViewModels.Playlist;

namespace zad1.Controllers
{
    public class PlaylistController : Controller
    {
        private readonly MusicDbContext context;
        public PlaylistController(MusicDbContext context)
        {
            this.context = context;
        }
        public async Task<IActionResult> Index()
        {
            var patients = await context.Playlists
                .Include(p => p.SongPlaylists)
                .ThenInclude(d => d.Song)
                .ToListAsync();
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
                var patient = new Playlist
                {
                    Title = model.Title,
                    Description = model.Description,
                };
                foreach (var doctorId in model.SongsIds)
                {
                    patient.SongPlaylists.Add(new SongPlaylist
                    {
                        SongId = doctorId,
                    });
                }
                context.Playlists.Add(patient);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadSongs(model.SongsIds);
            return View(model);
        }
        public async Task LoadSongs(List<int> selectedSongs = null) // Helper method to load doctors for the multi-select list.  
        {
            var doctors = await context.Songs.Select(d => new
            {
                d.Id,
                Name = d.Title + " " + d.ReleaseYear
            }).ToListAsync();
            ViewBag.Songs = new MultiSelectList(doctors, "Id", "Name", selectedSongs);
        }
        public async Task<IActionResult> Details(int id)
        {
            var patient = await context.Playlists
                .Include(p => p.SongPlaylists)
                .ThenInclude(dp => dp.Song)
                .FirstOrDefaultAsync(p => p.Id == id);
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
            var patient = await context.Playlists
                .Include(p => p.SongPlaylists)
                .FirstOrDefaultAsync(p => p.Id == id);
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
                var patient = await context.Playlists
                    .Include(p => p.SongPlaylists)
                    .FirstOrDefaultAsync(p => p.Id == model.Id);
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
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            await LoadSongs(model.SongIds);
            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var patient = await context.Playlists.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            context.Playlists.Remove(patient);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var patient = await context.Playlists.FindAsync(id);
            if (patient != null)
            {
                context.Playlists.Remove(patient);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
