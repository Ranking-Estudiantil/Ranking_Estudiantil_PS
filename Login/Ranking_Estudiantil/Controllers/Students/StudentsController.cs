using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ranking_Estudiantil.Models;

namespace Ranking_Estudiantil.Controllers.Students
{
    public class StudentsController : Controller
    {
        private readonly dbMyProjectContext _context;

        public StudentsController(dbMyProjectContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var dbMyProjectContext = _context.Students.Include(s => s.IdCareerNavigation).Include(s => s.IdNavigation).Include(s => s.IdRankNavigation);
            return View(await dbMyProjectContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.IdCareerNavigation)
                .Include(s => s.IdNavigation)
                .Include(s => s.IdRankNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["IdCareer"] = new SelectList(_context.Careers, "Id", "Id");
            ViewData["Id"] = new SelectList(_context.People, "Id", "Id");
            ViewData["IdRank"] = new SelectList(_context.Ranks, "Id", "Id");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Score,IdCareer,IdRank")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCareer"] = new SelectList(_context.Careers, "Id", "Id", student.IdCareer);
            ViewData["Id"] = new SelectList(_context.People, "Id", "Id", student.Id);
            ViewData["IdRank"] = new SelectList(_context.Ranks, "Id", "Id", student.IdRank);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["IdCareer"] = new SelectList(_context.Careers, "Id", "Id", student.IdCareer);
            ViewData["Id"] = new SelectList(_context.People, "Id", "Id", student.Id);
            ViewData["IdRank"] = new SelectList(_context.Ranks, "Id", "Id", student.IdRank);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,IdCareer,IdRank")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["IdCareer"] = new SelectList(_context.Careers, "Id", "Id", student.IdCareer);
            ViewData["Id"] = new SelectList(_context.People, "Id", "Id", student.Id);
            ViewData["IdRank"] = new SelectList(_context.Ranks, "Id", "Id", student.IdRank);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.IdCareerNavigation)
                .Include(s => s.IdNavigation)
                .Include(s => s.IdRankNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'dbMyProjectContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return _context.Students.Any(e => e.Id == id);
        }
    }
}
