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
    public class ComposersController : Controller
    {
        private readonly Database _context;

        public ComposersController(Database context)
        {
            _context = context;
        }

        // GET: Composers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Composer.ToListAsync());
        }

        // GET: Composers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (composer == null)
            {
                return NotFound();
            }

            return View(composer);
        }

        // GET: Composers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Composers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullName,Nationality,BirthDate")] Composer composer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(composer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(composer);
        }

        // GET: Composers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composer.FindAsync(id);
            if (composer == null)
            {
                return NotFound();
            }
            return View(composer);
        }

        // POST: Composers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,Nationality,BirthDate")] Composer composer)
        {
            if (id != composer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(composer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComposerExists(composer.ID))
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
            return View(composer);
        }

        // GET: Composers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var composer = await _context.Composer
                .FirstOrDefaultAsync(m => m.ID == id);
            if (composer == null)
            {
                return NotFound();
            }

            return View(composer);
        }

        // POST: Composers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var composer = await _context.Composer.FindAsync(id);
            _context.Composer.Remove(composer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComposerExists(int id)
        {
            return _context.Composer.Any(e => e.ID == id);
        }
    }
}
