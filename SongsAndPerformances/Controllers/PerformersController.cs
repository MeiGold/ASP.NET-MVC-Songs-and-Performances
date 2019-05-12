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
    public class PerformersController : Controller
    {
        private readonly Database _context;

        public PerformersController(Database context)
        {
            _context = context;
        }

        // GET: Performers
        public async Task<IActionResult> Index(string sortOrder, string SearchStringName, string SearchStringNationality)
        {
            ViewData["FullNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "fullname_desc" : "";
            ViewData["BirthDateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["NationalitySortParm"] = String.IsNullOrEmpty(sortOrder) ? "nationality_desc" : "";

            var performers = from s in _context.Performers
                        select s;

            if (!String.IsNullOrEmpty(SearchStringName))
            {
                performers = performers.Where(s => s.FullName.Contains(SearchStringName));
                ViewData["CurrentFullNameFilter"] = SearchStringName;
            }
            if (!String.IsNullOrEmpty(SearchStringNationality))
            {
                performers = performers.Where(s => s.Nationality.Contains(SearchStringNationality));
                ViewData["CurrentNationalityFilter"] = SearchStringNationality;
            }
            switch (sortOrder)
            {
                case "fullname_desc":
                    performers = performers.OrderByDescending(s => s.FullName);
                    break;
                case "Date":
                    performers = performers.OrderBy(s => s.BirthDate);
                    break;
                case "date_desc":
                    performers = performers.OrderByDescending(s => s.BirthDate);
                    break;
                case "nationality_desc":
                    performers = performers.OrderBy(s => s.Nationality);
                    break;
                default:
                    performers = performers.OrderBy(s => s.FullName);
                    break;
            }

            return View(await performers.AsNoTracking().ToListAsync());
        }

        // GET: Performers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (performer == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        // GET: Performers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Performers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FullName,BirthDate,Nationality")] Performer performer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(performer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(performer);
        }

        // GET: Performers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers.FindAsync(id);
            if (performer == null)
            {
                return NotFound();
            }
            return View(performer);
        }

        // POST: Performers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,BirthDate,Nationality")] Performer performer)
        {
            if (id != performer.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(performer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PerformerExists(performer.ID))
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
            return View(performer);
        }

        // GET: Performers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var performer = await _context.Performers
                .FirstOrDefaultAsync(m => m.ID == id);
            if (performer == null)
            {
                return NotFound();
            }

            return View(performer);
        }

        // POST: Performers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var performer = await _context.Performers.FindAsync(id);
            _context.Performers.Remove(performer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PerformerExists(int id)
        {
            return _context.Performers.Any(e => e.ID == id);
        }
    }
}
