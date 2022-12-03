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
    public class AcademicUnitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AcademicUnitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AcademicUnities
        public async Task<IActionResult> Index()
        {
              return View(await _context.AcademicUnities.ToListAsync());
        }

        // GET: AcademicUnities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AcademicUnities == null)
            {
                return NotFound();
            }

            var academicUnity = await _context.AcademicUnities
                .FirstOrDefaultAsync(m => m.AcademicUnityID == id);
            if (academicUnity == null)
            {
                return NotFound();
            }

            return View(academicUnity);
        }

        // GET: AcademicUnities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AcademicUnities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AcademicUnityName")] AcademicUnity academicUnity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(academicUnity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(academicUnity);
        }

        // GET: AcademicUnities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AcademicUnities == null)
            {
                return NotFound();
            }

            var academicUnity = await _context.AcademicUnities.FindAsync(id);
            if (academicUnity == null)
            {
                return NotFound();
            }
            return View(academicUnity);
        }

        // POST: AcademicUnities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AcademicUnityID,AcademicUnityName")] AcademicUnity academicUnity)
        {
            if (id != academicUnity.AcademicUnityID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(academicUnity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AcademicUnityExists(academicUnity.AcademicUnityID))
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
            return View(academicUnity);
        }

        // GET: AcademicUnities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AcademicUnities == null)
            {
                return NotFound();
            }

            var academicUnity = await _context.AcademicUnities
                .FirstOrDefaultAsync(m => m.AcademicUnityID == id);
            if (academicUnity == null)
            {
                return NotFound();
            }

            return View(academicUnity);
        }

        // POST: AcademicUnities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AcademicUnities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AcademicUnities'  is null.");
            }
            var academicUnity = await _context.AcademicUnities.FindAsync(id);
            if (academicUnity != null)
            {
                _context.AcademicUnities.Remove(academicUnity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcademicUnityExists(int id)
        {
          return _context.AcademicUnities.Any(e => e.AcademicUnityID == id);
        }
    }
}
