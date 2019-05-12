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
        public async Task<IActionResult> Index(string sortOrder, string SearchStringFullName, string SearchStringNationality, DateTime SearchStringBirthDate)
        {
            ViewData["FullNameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["NationalitySortParm"] = sortOrder == "Nationality" ? "nationality_desc" : "Nationality";
            ViewData["BirthDateSortParm"] = sortOrder == "BirthDate" ? "birthdate_desc" : "BirthDate";

            var composers = from s in _context.Composer
                        select s;

            if (!String.IsNullOrEmpty(SearchStringFullName))
            {
                composers = composers.Where(s => s.FullName.Contains(SearchStringFullName));
                ViewData["CurrentFullNameFilter"] = SearchStringFullName;
            }
            if (!String.IsNullOrEmpty(SearchStringNationality))
            {
                composers = composers.Where(s => s.Nationality.Contains(SearchStringNationality));
                ViewData["CurrentNationalityFilter"] = SearchStringNationality;
            }
            if (SearchStringBirthDate != null && SearchStringBirthDate != DateTime.Parse("1/1/0001 12:00:00 AM"))
            {
                composers = composers.Where(s => s.BirthDate.Equals(SearchStringBirthDate));
                ViewData["CurrentBirthDateFilter"] = SearchStringBirthDate.ToShortDateString();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    composers = composers.OrderByDescending(s => s.FullName);
                    break;
                case "Nationality":
                    composers = composers.OrderByDescending(s => s.Nationality);
                    break;
                case "nationality_desc":
                    composers = composers.OrderBy(s => s.Nationality);
                    break;
                case "BirthDate":
                    composers = composers.OrderBy(s => s.BirthDate);
                    break;
                case "birthdate_desc":
                    composers = composers.OrderByDescending(s => s.BirthDate);
                    break;
                default:
                    composers = composers.OrderBy(s => s.FullName);
                    break;
            }
            return View(await composers.AsNoTracking().ToListAsync());
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
