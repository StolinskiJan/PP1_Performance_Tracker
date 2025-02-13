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
using PTr.Data.Migrations;
using PTr.Models;

namespace PTr.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Tasks
   

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var exercises = await _context.Task
                .Include(e => e.TaskType)
                .Include(e => e.WorkSession)
                .Where(e => e.UserId == userId)
                .ToListAsync();
            return View(exercises);
        }



        // GET: Tasks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task = await _context.Task
                .Include(t => t.TaskType)
                .Include(t => t.User)
                .Include(t => t.WorkSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["TaskTypeId"] = new SelectList(_context.TaskType, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName");
            ViewData["WorkSessionId"] = new SelectList(_context.WorkSession.Where(s => s.UserId == userId), "Id", "LogIn");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hours,Days,Money,TaskTypeId,WorkSessionId,UserId")] Models.Task task)
        {
            task.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["TaskTypeId"] = new SelectList(_context.TaskType, "Id", "Id", task.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", task.UserId);
            ViewData["WorkSessionId"] = new SelectList(_context.WorkSession.Where(s => s.UserId == userId), "Id", "LogIn", task.WorkSessionId);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            //var task = await _context.Task.FindAsync(id);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _context.Task
                .Include(e => e.TaskType)
                .Include(e => e.WorkSession)
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            if (task == null)
            {
                return NotFound();
            }
            ViewData["TaskTypeId"] = new SelectList(_context.TaskType, "Id", "Name", task.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", task.UserId);
            ViewData["WorkSessionId"] = new SelectList(_context.WorkSession.Where(s => s.UserId == userId), "Id", "LogIn", task.WorkSessionId);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Hours,Days,Money,TaskTypeId,WorkSessionId,UserId")] Models.Task task)
        {
            if (id != task.Id)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingExercise = await _context.Task
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == userId);

            task.UserId = userId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.Id))
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
            ViewData["TaskTypeId"] = new SelectList(_context.TaskType, "Id", "Name", task.TaskTypeId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "UserName", task.UserId);
            ViewData["WorkSessionId"] = new SelectList(_context.WorkSession, "Id", "LogIn", task.WorkSessionId);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _context.Task
                .Include(t => t.TaskType)
                .Include(t => t.User)
                .Include(t => t.WorkSession)
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var task = await _context.Task.FindAsync(id);
            if (task != null)
            {
                _context.Task.Remove(task);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }
    }
}
