using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ranking_Estudiantil.Data;
using Ranking_Estudiantil.Models;

namespace Ranking_Estudiantil.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Bronze()
        {
           
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.career)
                                          where Student.Rank == 1
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Silver()
        {
           
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.career)
                                          where Student.Rank == 2
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Gold()
        {
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.career)
                                          where Student.Rank == 3
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Platinum()
        {
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.career)
                                          where Student.Rank == 4
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Diamond()
        {
           
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.career)
                                          where Student.Rank == 5
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Students.Include(s => s.PeronStud).Include(s => s.career);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.PeronStud)
                .Include(s => s.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "FirstName");
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,Rank,Score,CareerID")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "FirstName", student.PersonID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", student.CareerID);
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
            ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "FirstName", student.PersonID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", student.CareerID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,Rank,Score,CareerID")] Student student)
        {
            if (id != student.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    short newScore;
                    newScore = short.Parse(Request.Form["newScore"]);
                    student.Score = (short)(student.Score + newScore);
                    if(student.Rank == 1 && student.Score> 200)
                    {
                        student.Rank = 2;
                        student.Score = 0;
                    }else if (student.Rank == 2 && student.Score > 200){
                        student.Rank = 3;
                        student.Score = 0;
                    }else if(student.Rank == 3 && student.Score > 200)
                    {
                        student.Rank = 4;
                        student.Score = 0;
                    }else if(student.Rank == 4 && student.Score > 200)
                    {
                        student.Rank = 5;
                        student.Score = 0;
                    }
                 
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.PersonID))
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
            ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "FirstName ", student.PersonID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", student.CareerID);
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
                .Include(s => s.PeronStud)
                .Include(s => s.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
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
                return Problem("Entity set 'ApplicationDbContext.Students'  is null.");
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
          return _context.Students.Any(e => e.PersonID == id);
        }
    }
}
