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

            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.PeronStud.career)
                                          join Person in _context.People on Student.PersonID equals Person.PersonID
                                          join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                          join AcademicUnity in _context.AcademicUnities on Person.AcademicUnityID equals AcademicUnity.AcademicUnityID
                                          where Student.Rank == 1 && Person.Status == 1 && SessionClass.Carrera == Person.CareerID && SessionClass.unidadAcademica == Person.AcademicUnityID
                                          orderby Student.Score descending
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Silver()
        {

            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.PeronStud.career) 
                                          join Person in _context.People on Student.PersonID equals Person.PersonID
                                          join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                          join AcademicUnity in _context.AcademicUnities on Person.AcademicUnityID equals AcademicUnity.AcademicUnityID
                                          where Student.Rank == 2 && Person.Status == 1 && SessionClass.Carrera == Person.CareerID && SessionClass.unidadAcademica == Person.AcademicUnityID
                                          orderby Student.Score descending
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Gold()
        {
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.PeronStud.career)
                                          join Person in _context.People on Student.PersonID equals Person.PersonID
                                          join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                          join AcademicUnity in _context.AcademicUnities on Person.AcademicUnityID equals AcademicUnity.AcademicUnityID
                                          where Student.Rank == 3 && Person.Status == 1 && SessionClass.Carrera == Person.CareerID && SessionClass.unidadAcademica == Person.AcademicUnityID
                                          orderby Student.Score descending
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Platinum()
        {
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.PeronStud.career)
                                          join Person in _context.People on Student.PersonID equals Person.PersonID
                                          join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                          join AcademicUnity in _context.AcademicUnities on Person.AcademicUnityID equals AcademicUnity.AcademicUnityID
                                          where Student.Rank == 4 && Person.Status == 1 && SessionClass.Carrera == Person.CareerID && SessionClass.unidadAcademica == Person.AcademicUnityID
                                          orderby Student.Score descending
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Diamond()
        {

            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.PeronStud.career)
                                          join Person in _context.People on Student.PersonID equals Person.PersonID
                                          join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                          join AcademicUnity in _context.AcademicUnities on Person.AcademicUnityID equals AcademicUnity.AcademicUnityID
                                          where Student.Rank == 5 && Person.Status == 1 && SessionClass.Carrera == Person.CareerID && SessionClass.unidadAcademica == Person.AcademicUnityID
                                          orderby Student.Score descending
                                          select Student;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Logros(int id)
        {
            int aux = id;
            IQueryable<Projects> student = from Projects in _context.Projectss.Include(a => a.Student)

                                            where Projects.StudentsID == aux && Projects.Status == 0
                                            select Projects;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> LogrosDiamond(int id)
        {
            int aux = id;
            IQueryable<Projects> student = from Projects in _context.Projectss.Include(a => a.Student)

                                           where Projects.StudentsID == aux && Projects.Status == 0
                                           select Projects;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> LogrosSilver(int id)
        {
            int aux = id;
            IQueryable<Projects> student = from Projects in _context.Projectss.Include(a => a.Student)
                                           join Student in _context.Students on Projects.StudentsID equals Student.PersonID
                                           where Projects.StudentsID == aux && Projects.Status == 0
                                           select Projects;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> LogrosGold(int id)
        {
            int aux = id;
            IQueryable<Projects> student = from Projects in _context.Projectss.Include(a => a.Student)

                                           where Projects.StudentsID == aux && Projects.Status == 0
                                           select Projects;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> LogrosPlatinum(int id)
        {
            int aux = id;
            IQueryable<Projects> student = from Projects in _context.Projectss.Include(a => a.Student)

                                           where Projects.StudentsID == aux && Projects.Status == 0
                                           select Projects;


            return View(await student.ToListAsync());
        }
        public async Task<IActionResult> Index(string buscar)
        {
            IQueryable<Student> student = from Student in _context.Students.Include(a => a.PeronStud).Include(c => c.PeronStud.career)
                                          join Person in _context.People on Student.PersonID equals Person.PersonID
                                          join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                          join AcademicUnity in _context.AcademicUnities on Person.AcademicUnityID equals AcademicUnity.AcademicUnityID
                                          where Person.Status == 1 && SessionClass.Carrera == Person.CareerID && SessionClass.unidadAcademica == Person.AcademicUnityID
                                          select Student;

            if (!String.IsNullOrEmpty(buscar))
            {
                student = student.Where(s => s.PeronStud.FirstName!.Contains(buscar));
                
            }

            return View(await student.ToListAsync());
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
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,Rank,Score")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "FirstName", student.PersonID);
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
            return View(student);
        }

        
        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonID,Rank,Score")] Student student)
        {
            if (id != student.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string name = Request.Form["nombreProyecto"];
                    string achieve =Request.Form["achievment"];
                    double punctuation;
                    double total;
                    punctuation = double.Parse(Request.Form["rating"]);
                    if(achieve == "Logro Academico")
                    {
                        total = 500 * punctuation;
                    }
                    else
                    {
                        total = 200 * punctuation;

                    }
                    student.Score = (short)(student.Score + total);
                    if (student.Rank == 1 && student.Score > 600)
                    {
                        student.Rank = 2;
                        student.Score = 0;
                    }
                    else if (student.Rank == 2 && student.Score > 600)
                    {
                        student.Rank = 3;
                        student.Score = 0;
                    }
                    else if (student.Rank == 3 && student.Score > 600)
                    {
                        student.Rank = 4;
                        student.Score = 0;
                    }
                    else if (student.Rank == 4 && student.Score > 600)
                    {
                        student.Rank = 5;
                        student.Score = 0;
                    }
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    Projects r = new()
                    {
                        StudentsID = id,
                        ProjectName = name,
                        achievment = achieve,
                        punctuation = punctuation,
                        RegisterDate = DateTime.Now,
                        Status = 0


                    };
                    _context.Add(r);
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
            ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "FirstName", student.PersonID);
            return View(student);
        }
        // GET: Students/Edit1/5
        public async Task<IActionResult> Edit1(int? id)
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
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit1(int id, [Bind("PersonID,Rank,Score")] Student student)
        {
            if (id != student.PersonID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string description = Request.Form["descripcionSancion"];
                    int scoreSanction = int.Parse(Request.Form["puntajeSancion"]);
                   
                    student.Score = (short)(student.Score - scoreSanction);
                    if (student.Rank == 5 && student.Score <0)
                    {
                        student.Rank = 4;
                        student.Score = 500;
                    }
                    else if (student.Rank == 4 && student.Score < 0)
                    {
                        student.Rank = 3;
                        student.Score = 500;
                    }
                    else if (student.Rank == 3 && student.Score < 0)
                    {
                        student.Rank = 2;
                        student.Score = 500;
                    }
                    else if (student.Rank == 2 && student.Score < 0)
                    {
                        student.Rank = 1;
                        student.Score = 500;
                    }
                    else if (student.Rank == 1 && student.Score < 0)
                    {
                        student.Rank = 1;
                        student.Score = 0;
                    }
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                    Sanctions r = new()
                    {
                        StudentsID = id,
                        Description = description,
                        
                        punctuation = scoreSanction


                    };
                    _context.Add(r);
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
            ViewData["PersonID"] = new SelectList(_context.People, "PersonID", "FirstName", student.PersonID);
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
