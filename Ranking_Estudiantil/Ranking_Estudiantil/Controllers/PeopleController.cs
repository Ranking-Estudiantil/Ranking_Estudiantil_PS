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
    public class PeopleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PeopleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: People
        public async Task<IActionResult> Bronze()
        {

            IQueryable<Person> people = from Person in _context.People.Include(a => a.student).Include(c=>c.career)
                                        join Student in _context.Students on Person.PersonID equals Student.PersonID
                                          where Student.Rank == 1
                                          select Person;


            return View(await people.ToListAsync());
        }
        public async Task<IActionResult> Silver()
        {

            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud)
                                          join Person in _context.People on Student.PersonID equals Person.PersonID
                                          join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                          where Student.Rank == 2
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Gold()
        {
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud)
                                          where Student.Rank == 3
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Platinum()
        {
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud)
                                          where Student.Rank == 4
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Diamond()
        {

            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud)
                                          where Student.Rank == 5
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.People.Include(p => p.academicUnity).Include(p => p.career);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: People/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People
                .Include(p => p.academicUnity)
                .Include(p => p.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: People/Create
        public IActionResult Create()
        {
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName");
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FirstName,LastName,SecondLastName,AcademicUnityID,CareerID,Status,RegisterDate,Role,Email,Username,Password")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", person.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", person.CareerID);
            return View(person);
        }

        // GET: People/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", person.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", person.CareerID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,FirstName,LastName,SecondLastName,AcademicUnityID,CareerID,Status,RegisterDate,Role,Email,Username,Password")] Person person)
        {
            if (id != person.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", person.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", person.CareerID);
            return View(person);
        }

        // GET: People/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.People == null)
            {
                return NotFound();
            }

            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", person.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", person.CareerID);
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [Bind("PersonID,FirstName,LastName,SecondLastName,AcademicUnityID,CareerID,Status,RegisterDate,Role,Email,Username,Password")] Person person)
        {
            if (id != person.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                person.Status = 0;
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                
             
                return RedirectToAction(nameof(Index));
            }
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", person.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", person.CareerID);
            return View(person);
        }
    }
}
