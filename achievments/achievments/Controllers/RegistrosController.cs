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
    public class RegistrosController : Controller
    {
        private readonly AchieveContext _context;

        public RegistrosController(AchieveContext context)
        {
            _context = context;
        }

        // GET: Registros
        public async Task<IActionResult> Index()
        {
              return View(await _context.registros.ToListAsync());
        }

        // GET: Registros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.registros == null)
            {
                return NotFound();
            }

            var registros = await _context.registros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registros == null)
            {
                return NotFound();
            }

            return View(registros);
        }

        // GET: Registros/Create
        public IActionResult Create()
        {
            ViewData["achievmentID"] = new SelectList(_context.Achievments, "Id", "Name");
            return View();
        }

        // POST: Registros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Score,achievmentID")] Registros registros)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registros);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["achievmentID"] = new SelectList(_context.Achievments, "Id", "Name", registros.achievmentID);
            return View(registros);
        }

        // GET: Registros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.registros == null)
            {
                return NotFound();
            }

            var registros = await _context.registros.FindAsync(id);
            if (registros == null)
            {
                return NotFound();
            }
            return View(registros);
        }

        // POST: Registros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Score,achievmentID")] Registros registros)
        {
            if (id != registros.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registros);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistrosExists(registros.Id))
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
            return View(registros);
        }

        // GET: Registros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.registros == null)
            {
                return NotFound();
            }

            var registros = await _context.registros
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registros == null)
            {
                return NotFound();
            }

            return View(registros);
        }

        // POST: Registros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.registros == null)
            {
                return Problem("Entity set 'AchieveContext.registros'  is null.");
            }
            var registros = await _context.registros.FindAsync(id);
            if (registros != null)
            {
                _context.registros.Remove(registros);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistrosExists(int id)
        {
          return _context.registros.Any(e => e.Id == id);
        }
    }
}
