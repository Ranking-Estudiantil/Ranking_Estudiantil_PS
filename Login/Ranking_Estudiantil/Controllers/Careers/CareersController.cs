using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ranking_Estudiantil.Models;

namespace Ranking_Estudiantil.Controllers.Careers
{
    public class CareersController : Controller
    {
        private readonly dbMyProjectContext _context;

        public CareersController(dbMyProjectContext context)
        {
            _context = context;
        }

        // GET: Careers
        public async Task<IActionResult> Index()
        {
            var dbMyProjectContext = _context.Careers.Include(c => c.IdDepartmentNavigation);
            return View(await dbMyProjectContext.ToListAsync());
        }

        // GET: Careers/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Careers == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .Include(c => c.IdDepartmentNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // GET: Careers/Create
        public IActionResult Create()
        {
            ViewData["IdDepartment"] = new SelectList(_context.Departments, "Id", "Id");
            return View();
        }

        // POST: Careers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,IdDepartment")] Career career)
        {
            if (ModelState.IsValid)
            {
                _context.Add(career);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartment"] = new SelectList(_context.Departments, "Id", "Id", career.IdDepartment);
            return View(career);
        }

        // GET: Careers/Edit/5
        public async Task<IActionResult> Edit(byte? id)
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
            ViewData["IdDepartment"] = new SelectList(_context.Departments, "Id", "Id", career.IdDepartment);
            return View(career);
        }

        // POST: Careers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("Id,Name,IdDepartment")] Career career)
        {
            if (id != career.Id)
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
                    if (!CareerExists(career.Id))
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
            ViewData["IdDepartment"] = new SelectList(_context.Departments, "Id", "Id", career.IdDepartment);
            return View(career);
        }

        // GET: Careers/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Careers == null)
            {
                return NotFound();
            }

            var career = await _context.Careers
                .Include(c => c.IdDepartmentNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (career == null)
            {
                return NotFound();
            }

            return View(career);
        }

        // POST: Careers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Careers == null)
            {
                return Problem("Entity set 'dbMyProjectContext.Careers'  is null.");
            }
            var career = await _context.Careers.FindAsync(id);
            if (career != null)
            {
                _context.Careers.Remove(career);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CareerExists(byte id)
        {
          return _context.Careers.Any(e => e.Id == id);
        }
    }
}
