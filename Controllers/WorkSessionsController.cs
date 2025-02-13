using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PTr.Data;
using PTr.Models;

namespace PTr.Controllers
{
    [Authorize]
    public class WorkSessionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkSessionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkSessions
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(await _context.WorkSession.Include(s => s.User).Where(s => s.UserId == userId).ToListAsync());
        }

        // GET: WorkSessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workSession = await _context.WorkSession
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (workSession == null)
            {
                return NotFound();
            }

            return View(workSession);
        }

        // GET: WorkSessions/Create
        public IActionResult Create()
        {
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: WorkSessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
   

        public async Task<IActionResult> Create([Bind("Id,LogIn,LogOut")] WorkSession workSession)
        {
            workSession.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ModelState.IsValid)
            {
                _context.Add(workSession);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(workSession);
        }

        // GET: WorkSessions/Edit/5

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workSession = await _context.WorkSession
                   .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (workSession == null)
            {
                return NotFound();
            }

            return View(workSession);
        }

        // POST: WorkSessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     

        public async Task<IActionResult> Edit(int id, [Bind("Id,LogIn,LogOut")] WorkSession workSession)
        {
            if (id != workSession.Id)
            {
                return NotFound();
            }


            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingSession = await _context.WorkSession
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (existingSession == null)
            {
                return Forbid();
            }

            workSession.UserId = userId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workSession);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkSessionExists(workSession.Id))
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
            return View(workSession);
        }



        // GET: WorkSessions/Delete/5
    

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workSession = await _context.WorkSession
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (workSession == null)
            {
                return NotFound();
            }

            return View(workSession);
        }

        // POST: WorkSessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var workSession = await _context.WorkSession
                .FirstOrDefaultAsync(s => s.Id == id && s.UserId == userId);

            if (workSession != null)
            {
                return Forbid();
            }

            _context.WorkSession.Remove(workSession);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkSessionExists(int id)
        {
            return _context.WorkSession.Any(e => e.Id == id);
        }
    }
}
