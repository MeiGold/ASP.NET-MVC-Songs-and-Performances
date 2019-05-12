using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Songs_and_Performances.Data;
using Songs_and_Performances.Models;

namespace SongsAndPerformances.Controllers
{
    public class SongsController : Controller
    {
        private readonly Database _context;

        public SongsController(Database context)
        {
            _context = context;
        }

        // GET: Songs
        public async Task<IActionResult> Index(string sortOrder, string searchStringName, string searchStringGenre)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["GenreSortParm"] = sortOrder == "Genre" ? "genre_desc" : "Genre";
            ViewData["DurationSortParm"] = sortOrder == "Duration" ? "duration_desc" : "Duration";

            var songs = from s in _context.Songs
                        select s;

            if (!String.IsNullOrEmpty(searchStringName))
            {
                songs = songs.Where(s => s.Name.Contains(searchStringName));
                ViewData["CurrentNameFilter"] = searchStringName;
            }
            if (!String.IsNullOrEmpty(searchStringGenre))
            {
                songs = songs.Where(s => s.Genre.Contains(searchStringGenre));
                ViewData["CurrentGenreFilter"] = searchStringGenre;
            }

            switch (sortOrder)
            {
                case "name_desc":
                    songs = songs.OrderByDescending(s => s.Name);
                    break;
                case "Genre":
                    songs=songs.OrderByDescending(s => s.Genre);
                    break;
                case "genre_desc":
                    songs = songs.OrderBy(s => s.Genre);
                    break;
                case "Duration":
                    songs = songs.OrderBy(s => s.Duration);
                    break;
                case "duration_desc":
                    songs = songs.OrderByDescending(s => s.Duration);
                    break;
                default:
                    songs = songs.OrderBy(s => s.Name);
                    break;
            }

            return View(await songs.AsNoTracking().ToListAsync());
        }

        // GET: Songs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // GET: Songs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Genre,Duration")] Song song)
        {
            if (ModelState.IsValid)
            {
                _context.Add(song);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Genre,Duration")] Song song)
        {
            if (id != song.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(song);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SongExists(song.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(song);
        }

        // GET: Songs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var song = await _context.Songs
                .FirstOrDefaultAsync(m => m.ID == id);
            if (song == null)
            {
                return NotFound();
            }

            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            _context.Songs.Remove(song);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SongExists(int id)
        {
            return _context.Songs.Any(e => e.ID == id);
        }
    }
}
