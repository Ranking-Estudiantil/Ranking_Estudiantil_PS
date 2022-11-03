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
    public class PersonStudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonStudents
        public async Task<IActionResult> Index()
        {

            IQueryable<Person> student = from Person in _context.People.Include(a => a.student).Include(c => c.student.career)
                                          where Person.Role == 2
                                          select Person;


            return View(await student.ToListAsync());
        }

        // GET: PersonStudents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonStudent == null)
            {
                return NotFound();
            }

            var personStudent = await _context.PersonStudent
                .Include(p => p.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (personStudent == null)
            {
                return NotFound();
            }

            return View(personStudent);
        }

        // GET: PersonStudents/Create
        public IActionResult Create()
        {
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName");
            return View();
        }

        // POST: PersonStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FirstName,LastName,SecondLastName,Status,RegisterDate,Role,Email,Password,StudentID,Rank,Score,CareerID")] PersonStudent personStudent)
        {
            if (ModelState.IsValid)
            {
                Person p = new()
                {
                    FirstName = personStudent.FirstName,
                    LastName = personStudent.LastName,
                    SecondLastName = personStudent.SecondLastName,
                    Status = 1,
                    RegisterDate = DateTime.Now,
                    Role = 2,
                    Email = personStudent.Email,
                    Password = personStudent.Password

                     
                };
                _context.Add(p);
                await _context.SaveChangesAsync();

                Student s = new()
                {
                    PersonID = p.PersonID,
                    Rank = 1,
                    Score = 0,
                    CareerID = personStudent.CareerID
                };
                _context.Add(s);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            return View(personStudent);
        }

        // GET: PersonStudents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonStudent == null)
            {
                return NotFound();
            }

            var personStudent = await _context.PersonStudent.FindAsync(id);
            if (personStudent == null)
            {
                return NotFound();
            }
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personStudent.CareerID);
            return View(personStudent);
        }

        // POST: PersonStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,FirstName,LastName,SecondLastName,Status,RegisterDate,Role,Email,Password,StudentID,Rank,Score,CareerID")] PersonStudent personStudent)
        {
            if (id != personStudent.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personStudent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personStudent.CareerID);
            return View(personStudent);
        }

        // GET: PersonStudents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonStudent == null)
            {
                return NotFound();
            }

            var personStudent = await _context.PersonStudent
                .Include(p => p.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (personStudent == null)
            {
                return NotFound();
            }

            return View(personStudent);
        }

        // POST: PersonStudents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PersonStudent == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PersonStudent'  is null.");
            }
            var personStudent = await _context.PersonStudent.FindAsync(id);
            if (personStudent != null)
            {
                _context.PersonStudent.Remove(personStudent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonStudentExists(int id)
        {
          return _context.PersonStudent.Any(e => e.PersonID == id);
        }
    }
}
