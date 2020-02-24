using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamProject.Data;
using TeamProject.Models;

namespace TeamProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SummaryController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<User> _userManager;

        public SummaryController(ApplicationDbContext context, UserManager<User>userManager)
        {
            _context = context;
            _userManager = userManager;

        }



        private Task<User> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        // GET: Summary
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            //var applicationDbContext = _context.Summaries.Include(s => s.Tour).Include(s => s.User);
            var applicationDbContext = _context.Summaries
                .Where(s => s.UserId == user.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Summary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summary = await _context.Summaries
                .Include(s => s.Tour)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (summary == null)
            {
                return NotFound();
            }

            return View(summary);
        }

        // GET: Summary/Create
        public async Task<IActionResult> CreateAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Tour = await _context.Tours
                .FirstOrDefaultAsync(t => t.TourId == id);
            if (Tour == null)
            {
                return NotFound();
            }
            //var summary = await _context.Summaries
            //    .Include(s => s.Tour)
            //    //.Include(s => s.User)
            //    .FirstOrDefaultAsync(m => m.TourId == id);
            //if (summary == null)
            //{
            //    return NotFound();
            //}
            var user = await GetCurrentUserAsync();
            ViewData["TourId"] = id;

            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UserId"] = user.Id;
            return View(Tour);
        }

        // POST: Summary/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TourId,UserId")] Summary summary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(summary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Destination", summary.TourId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", summary.UserId);
            return View(summary);
        }

        // GET: Summary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summary = await _context.Summaries.FindAsync(id);
            if (summary == null)
            {
                return NotFound();
            }
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Destination", summary.TourId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", summary.UserId);
            return View(summary);
        }

        // POST: Summary/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TourId,UserId")] Summary summary)
        {
            if (id != summary.TourId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(summary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SummaryExists(summary.TourId))
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
            ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Destination", summary.TourId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", summary.UserId);
            return View(summary);
        }

        // GET: Summary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var summary = await _context.Summaries
                .Include(s => s.Tour)
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (summary == null)
            {
                return NotFound();
            }

            return View(summary);
        }

        // POST: Summary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var summary = await _context.Summaries.FindAsync(id);
            _context.Summaries.Remove(summary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SummaryExists(int id)
        {
            return _context.Summaries.Any(e => e.TourId == id);
        }
    }
}
