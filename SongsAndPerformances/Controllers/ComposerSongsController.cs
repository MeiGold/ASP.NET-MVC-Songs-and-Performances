using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SongsAndPerformances.Models;
using Songs_and_Performances.Data;

namespace SongsAndPerformances.Controllers
{
    public class ComposerSongsController : Controller
    {
        private readonly Database _context;

        public ComposerSongsController(Database context)
        {
            _context = context;
        }

        // GET: ComposerSongs
        public async Task<IActionResult> Index()
        {
            var database = _context.ComposerSong.Include(c => c.Composer).Include(c => c.Song);
            return View(await database.ToListAsync());
        }

        // GET: ComposerSongs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composerSong = await _context.ComposerSong
                .Include(c => c.Composer)
                .Include(c => c.Song)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (composerSong == null)
            {
                return NotFound();
            }

            return View(composerSong);
        }

        // GET: ComposerSongs/Create
        public IActionResult Create()
        {
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "FullName");
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "Name");
            return View();
        }

        // POST: ComposerSongs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SongID,ComposerID")] ComposerSong composerSong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(composerSong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "FullName", composerSong.ComposerID);
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "Name", composerSong.SongID);
            return View(composerSong);
        }

        // GET: ComposerSongs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composerSong = await _context.ComposerSong.FindAsync(id);
            if (composerSong == null)
            {
                return NotFound();
            }
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "FullName", composerSong.ComposerID);
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "Name", composerSong.SongID);
            return View(composerSong);
        }

        // POST: ComposerSongs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SongID,ComposerID")] ComposerSong composerSong)
        {
            if (id != composerSong.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(composerSong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComposerSongExists(composerSong.ID))
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
            ViewData["ComposerID"] = new SelectList(_context.Composer, "ID", "FullName", composerSong.ComposerID);
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "Name", composerSong.SongID);
            return View(composerSong);
        }

        // GET: ComposerSongs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composerSong = await _context.ComposerSong
                .Include(c => c.Composer)
                .Include(c => c.Song)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (composerSong == null)
            {
                return NotFound();
            }

            return View(composerSong);
        }

        // POST: ComposerSongs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var composerSong = await _context.ComposerSong.FindAsync(id);
            _context.ComposerSong.Remove(composerSong);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComposerSongExists(int id)
        {
            return _context.ComposerSong.Any(e => e.ID == id);
        }
    }
}
