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
    public class UsersController : Controller
    {
        private readonly Database _context;

        public UsersController(Database context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.User.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nickname,Password,Type")] User user)
        {
            user.Type = (TypeOfUser)3;
            int count = _context.User.ToArray().Count();

            var fyu = from s in _context.User where s.Nickname == user.Nickname select s;
            var a = fyu.ToArray();
            if (!a.Any())
            {
                return RedirectToAction("False");
            }
            

            if (ModelState.IsValid)
            {
                
                _context.Add(user);
                await _context.SaveChangesAsync();
                
                return RedirectToAction("Index","Home");
            }

            return View();
            
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nickname,Password,Type")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
        public async Task<IActionResult> False()
        {
            return View();
        }
        public  IActionResult Logging()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logging([Bind("ID,Nickname,Password,Type")] User user)
        {

            // ViewData["Nickname"] = inputNickname;
            //ViewData["Password"] = inputPassword;
            string inputNickname = user.Nickname;
            string inputPassword = user.Password;


            var database = (from s in _context.User where s.Nickname == inputNickname && s.Password == inputPassword select s);
            var sd = database.ToArray();
            if (sd.Any())
            {
                
                return RedirectToAction("Index", "Home");
            }
            
            return RedirectToAction("Falselogging");
        }
        public async Task<IActionResult> Falselogging()
        {
            return View();
        }

    }
}
