using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using achievments.Models;

namespace achievments.Controllers
{
    public class AchievmentsController : Controller
    {
        private readonly AchieveContext _context;

        public AchievmentsController(AchieveContext context)
        {
            _context = context;
        }

        // GET: Achievments
        public async Task<IActionResult> Index()
        {
              return View(await _context.Achievments.ToListAsync());
        }

        // GET: Achievments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Achievments == null)
            {
                return NotFound();
            }

            var achievments = await _context.Achievments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achievments == null)
            {
                return NotFound();
            }

            return View(achievments);
        }

        // GET: Achievments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Achievments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,description")] Achievments achievments)
        {
            if (ModelState.IsValid)
            {
                _context.Add(achievments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(achievments);
        }

        // GET: Achievments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Achievments == null)
            {
                return NotFound();
            }

            var achievments = await _context.Achievments.FindAsync(id);
            if (achievments == null)
            {
                return NotFound();
            }
            return View(achievments);
        }

        // POST: Achievments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,description")] Achievments achievments)
        {
            if (id != achievments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achievments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchievmentsExists(achievments.Id))
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
            return View(achievments);
        }

        // GET: Achievments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Achievments == null)
            {
                return NotFound();
            }

            var achievments = await _context.Achievments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achievments == null)
            {
                return NotFound();
            }

            return View(achievments);
        }

        // POST: Achievments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Achievments == null)
            {
                return Problem("Entity set 'AchieveContext.Achievments'  is null.");
            }
            var achievments = await _context.Achievments.FindAsync(id);
            if (achievments != null)
            {
                _context.Achievments.Remove(achievments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AchievmentsExists(int id)
        {
          return _context.Achievments.Any(e => e.Id == id);
        }
    }
}
