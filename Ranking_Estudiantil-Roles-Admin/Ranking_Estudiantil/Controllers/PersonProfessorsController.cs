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
    public class PersonProfessorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonProfessorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonProfessors
        public async Task<IActionResult> Index()
        {
            IQueryable<Person> student = from Person in _context.People.Include(a => a.student).Include(c => c.student.career)
                                         where Person.Role != 2
                                         select Person;


            return View(await student.ToListAsync());
        }

        // GET: PersonProfessors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PersonProfessor == null)
            {
                return NotFound();
            }

            var personProfessor = await _context.PersonProfessor
                .Include(p => p.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (personProfessor == null)
            {
                return NotFound();
            }

            return View(personProfessor);
        }

        // GET: PersonProfessors/Create
        public IActionResult Create()
        {
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName");
            return View();
        }

        // POST: PersonProfessors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FirstName,LastName,SecondLastName,Status,RegisterDate,Role,Email,Password,ProfessorID,CareerID")] PersonProfessor personProfessor)
        {
            if (ModelState.IsValid)
            {
                Person p = new()
                {
                    FirstName = personProfessor.FirstName,
                    LastName = personProfessor.LastName,
                    SecondLastName = personProfessor.SecondLastName,
                    Status = 1,
                    RegisterDate = DateTime.Now,
                    Role = personProfessor.Role,
                    Email = personProfessor.Email,
                    Password = personProfessor.Password


                };
                _context.Add(p);
                await _context.SaveChangesAsync();

                Professor pr = new()
                {
                    PersonID = p.PersonID,
                    CareerID = personProfessor.CareerID
                };
                _context.Add(pr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personProfessor.CareerID);
            return View(personProfessor);
        }

        // GET: PersonProfessors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PersonProfessor == null)
            {
                return NotFound();
            }

            var personProfessor = await _context.PersonProfessor.FindAsync(id);
            if (personProfessor == null)
            {
                return NotFound();
            }
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personProfessor.CareerID);
            return View(personProfessor);
        }

        // POST: PersonProfessors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PersonID,FirstName,LastName,SecondLastName,Status,RegisterDate,Role,Email,Password,ProfessorID,CareerID")] PersonProfessor personProfessor)
        {
            if (id != personProfessor.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personProfessor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonProfessorExists(personProfessor.PersonID))
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
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personProfessor.CareerID);
            return View(personProfessor);
        }

        // GET: PersonProfessors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PersonProfessor == null)
            {
                return NotFound();
            }

            var personProfessor = await _context.PersonProfessor
                .Include(p => p.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (personProfessor == null)
            {
                return NotFound();
            }

            return View(personProfessor);
        }

        // POST: PersonProfessors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (_context.PersonProfessor == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PersonProfessor'  is null.");
            }
            var personProfessor = await _context.PersonProfessor.FindAsync(id);
            if (personProfessor != null)
            {
                _context.PersonProfessor.Remove(personProfessor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonProfessorExists(int? id)
        {
          return _context.PersonProfessor.Any(e => e.PersonID == id);
        }
    }
}
