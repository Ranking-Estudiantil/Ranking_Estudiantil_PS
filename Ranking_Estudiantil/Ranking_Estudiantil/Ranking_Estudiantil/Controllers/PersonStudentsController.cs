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
        Random random = new Random();
        public PersonStudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonStudents
        public async Task<IActionResult> Index()
        {
            IQueryable<Person> student = from Person in _context.People.Include(a => a.student).Include(c => c.career).Include(a => a.academicUnity)

                                         where Person.Role == "Estudiante"
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
                .Include(p => p.academicUnity)
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
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName");
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName");
            return View();
        }

        // POST: PersonStudents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FirstName,LastName,SecondLastName,AcademicUnityID,CareerID,Status,RegisterDate,Role,Email,Username,Password,StudentID,Rank,Score")] PersonStudent personStudent)
        {
           
                string password = string.Empty;
                for (int i = 0; i < 7; i++)
                {
                    int pass = random.Next(0, 10);
                     password += pass.ToString();
                }
                personStudent.Password = password;
                string userName = string.Empty;
                string firstName = Request.Form["FirstName"];
                string lastName = Request.Form["LastName"];
                string secondLast = Request.Form["SecondLastName"];
                for (int i = 0; i < lastName.Length; i++)
            {
                if (i == 0)
                {
                    userName += lastName[0];
                }
            }

            for (int i = 0; i < secondLast.Length; i++)
            {
                if (i == 0)
                {
                        if(secondLast == null)
                        {
                            userName += "x";
                        }
                        else
                        {
                            userName += secondLast[0];
                        }
                   
                }
            }

            for (int i = 0; i < firstName.Length; i++)
            {
                if (i == 0)
                {
                    userName += firstName[0];
                }
            }
            for (int i = 0; i < 7; i++)
            {
                int pass = random.Next(0, 10);
                userName += pass.ToString();
            }
            personStudent.Username = userName;
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Person p = new()
                        {
                            FirstName = personStudent.FirstName,
                            LastName = personStudent.LastName,
                            SecondLastName = personStudent.SecondLastName,
                            AcademicUnityID = personStudent.AcademicUnityID,
                            CareerID = personStudent.CareerID,
                            Status = 1,
                            RegisterDate = DateTime.Now,
                            Role = "Estudiante",
                            Email = personStudent.Email,
                            Username = userName,
                            Password = password
                            


                        };
                        _context.Add(p);
                        await _context.SaveChangesAsync();

                        Student s = new()
                        {
                            PersonID = p.PersonID,
                            Rank = 1,
                            Score = 0,

                        };
                        _context.Add(s);
                        await _context.SaveChangesAsync();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
                    return RedirectToAction(nameof(Index));
            
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", personStudent.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personStudent.CareerID);
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
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", personStudent.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personStudent.CareerID);
            return View(personStudent);
        }

        // POST: PersonStudents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PersonID,FirstName,LastName,SecondLastName,AcademicUnityID,CareerID,Status,RegisterDate,Role,Email,Username,Password,StudentID,Rank,Score")] PersonStudent personStudent)
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
                    if (!PersonStudentExists(personStudent.PersonID))
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
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", personStudent.AcademicUnityID);
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
                .Include(p => p.academicUnity)
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
        public async Task<IActionResult> DeleteConfirmed(int? id)
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

        private bool PersonStudentExists(int? id)
        {
          return _context.PersonStudent.Any(e => e.PersonID == id);
        }
    }
}
