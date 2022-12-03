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
    public class SanctionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SanctionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sanctions
        public async Task<IActionResult> Index(int id)
        {
            int aux = id;
            IQueryable<Sanctions> student = from Sanctions in _context.Sanctionss.Include(a => a.Student)

                                           where Sanctions.StudentsID == aux
                                           select Sanctions;


            return View(await student.ToListAsync());
        }

        // GET: Sanctions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sanctionss == null)
            {
                return NotFound();
            }

            var sanctions = await _context.Sanctionss
                .FirstOrDefaultAsync(m => m.SanctionsID == id);
            if (sanctions == null)
            {
                return NotFound();
            }

            return View(sanctions);
        }

        // GET: Sanctions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sanctions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SanctionsID,StudentsID,Description,punctuation")] Sanctions sanctions)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sanctions);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sanctions);
        }

        // GET: Sanctions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sanctionss == null)
            {
                return NotFound();
            }

            var sanctions = await _context.Sanctionss.FindAsync(id);
            if (sanctions == null)
            {
                return NotFound();
            }
            return View(sanctions);
        }

        // POST: Sanctions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SanctionsID,StudentsID,Description,punctuation")] Sanctions sanctions)
        {
            if (id != sanctions.SanctionsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanctions);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanctionsExists(sanctions.SanctionsID))
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
            return View(sanctions);
        }

        // GET: Sanctions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sanctionss == null)
            {
                return NotFound();
            }

            var sanctions = await _context.Sanctionss
                .FirstOrDefaultAsync(m => m.SanctionsID == id);
            if (sanctions == null)
            {
                return NotFound();
            }

            return View(sanctions);
        }

        // POST: Sanctions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sanctionss == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sanctionss'  is null.");
            }
            var sanctions = await _context.Sanctionss.FindAsync(id);
            if (sanctions != null)
            {
                _context.Sanctionss.Remove(sanctions);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanctionsExists(int id)
        {
          return _context.Sanctionss.Any(e => e.SanctionsID == id);
        }
    }
}
