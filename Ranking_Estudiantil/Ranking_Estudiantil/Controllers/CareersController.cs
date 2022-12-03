using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ranking_Estudiantil.Data;
using Ranking_Estudiantil.Models;

namespace Ranking_Estudiantil.Controllers
{
    [Authorize]
    public class CareersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CareersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Careers
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Careers.Include(c => c.Department);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Careers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Careers == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.CareerID == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // GET: Careers/Create
        public IActionResult Create()
        {
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName");
            return View();
        }

        // POST: Careers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CareerName,DepartmentID")] Career career)
        {
            if (ModelState.IsValid)
            {
                _context.Add(career);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", career.DepartmentID);
            return View(career);
        }

        // GET: Careers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Careers == null)
            {
                return NotFound();
            }

            var career = await _context.Careers.FindAsync(id);
            if (career == null)
            {
                return NotFound();
            }
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", career.DepartmentID);
            return View(career);
        }

        // POST: Careers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CareerID,CareerName,DepartmentID")] Career career)
        {
            if (id != career.CareerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(career);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CareerExists(career.CareerID))
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
            ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "DepartmentName", career.DepartmentID);
            return View(career);
        }

        // GET: Careers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Careers == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .Include(c => c.Department)
                .FirstOrDefaultAsync(m => m.CareerID == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Careers == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Careers'  is null.");
            }
            var career = await _context.Careers.FindAsync(id);
            if (career != null)
            {
                _context.Careers.Remove(career);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareerExists(int id)
        {
          return _context.Careers.Any(e => e.CareerID == id);
        }
    }
}
