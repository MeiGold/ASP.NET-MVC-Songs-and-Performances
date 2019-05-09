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
    public class PerformancesController : Controller
    {
        private readonly Database _context;

        public PerformancesController(Database context)
        {
            _context = context;
        }

        // GET: Performances
        public async Task<IActionResult> Index()
        {
            var database = _context.Performances.Include(p => p.Performer).Include(p => p.Song);
            return View(await database.ToListAsync());
        }

        // GET: Performances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performances
                .Include(p => p.Performer)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // GET: Performances/Create
        public IActionResult Create()
        {
            ViewData["PerformerID"] = new SelectList(_context.Performers, "ID", "FullName");
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "Name");
            return View();
        }

        // POST: Performances/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SongID,PerformerID,Name,Date,Place")] Performance performance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PerformerID"] = new SelectList(_context.Performers, "ID", "ID", performance.PerformerID);
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "ID", performance.SongID);
            return View(performance);
        }

        // GET: Performances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performances.FindAsync(id);
            if (performance == null)
            {
                return NotFound();
            }
            ViewData["PerformerID"] = new SelectList(_context.Performers, "ID", "FullName", performance.PerformerID);
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "Name", performance.SongID);
            return View(performance);
        }

        // POST: Performances/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SongID,PerformerID,Name,Date,Place")] Performance performance)
        {
            if (id != performance.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformanceExists(performance.ID))
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
            ViewData["PerformerID"] = new SelectList(_context.Performers, "ID", "ID", performance.PerformerID);
            ViewData["SongID"] = new SelectList(_context.Songs, "ID", "ID", performance.SongID);
            return View(performance);
        }

        // GET: Performances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performance = await _context.Performances
                .Include(p => p.Performer)
                .Include(p => p.Song)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (performance == null)
            {
                return NotFound();
            }

            return View(performance);
        }

        // POST: Performances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performance = await _context.Performances.FindAsync(id);
            _context.Performances.Remove(performance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformanceExists(int id)
        {
            return _context.Performances.Any(e => e.ID == id);
        }
    }
}
