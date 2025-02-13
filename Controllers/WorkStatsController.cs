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
    public class WorkStatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkStatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: WorkStats
        public async Task<IActionResult> Index()
        {
            var fourWeeksAgo = DateTime.Now.AddDays(-28);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var workStats = await _context.Task
                .Include(t => t.TaskType)
                .Include(t => t.WorkSession)
                .Where(e => e.WorkSession.LogIn >= fourWeeksAgo && e.UserId == userId)
                .Select(t => new WorkStat
                {
                    Id = t.Id,
                    TaskTypeId = t.TaskTypeId,
                    TaskType = t.TaskType,
                    NumberOfHoursWorked = t.Hours * t.Days,
                    WorkMoney = t.Hours * t.Days * t.Money
                })
                .ToListAsync();

            return View(workStats);
        }


    }
}
