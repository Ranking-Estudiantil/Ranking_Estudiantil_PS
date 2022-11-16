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
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public async Task<IActionResult> Index(int id)
        {
            int aux = id;
            IQueryable<Projects> student = from Projects in _context.Projectss.Include(a => a.Student)
                                          
                                          where Projects.StudentsID == aux
                                          select Projects;


            return View(await student.ToListAsync());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Projectss == null)
            {
                return NotFound();
            }

            var projects = await _context.Projectss
                .FirstOrDefaultAsync(m => m.ProjectsID == id);
            if (projects == null)
            {
                return NotFound();
            }

            return View(projects);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectsID,StudentsID,achievment,ProjectName,punctuation,Status,RegisterDate")] Projects projects)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projects);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(projects);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Projectss == null)
            {
                return NotFound();
            }

            var projects = await _context.Projectss.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }
            return View(projects);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectsID,StudentsID,achievment,ProjectName,punctuation,Status,RegisterDate")] Projects projects)
        {
            if (id != projects.ProjectsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(projects.ProjectsID))
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
            return View(projects);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Projectss == null)
            {
                return NotFound();
            }

            var projects = await _context.Projectss.FindAsync(id);
            if (projects == null)
            {
                return NotFound();
            }
            return View(projects);
        }

        // POST: Projects/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, [Bind("ProjectsID,StudentsID,achievment,ProjectName,punctuation,Status,RegisterDate")] Projects projects)
        {
            if (id != projects.ProjectsID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    projects.Status = 1;
                    _context.Update(projects);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectsExists(projects.ProjectsID))
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
            return View(projects);
        }

        private bool ProjectsExists(int id)
        {
          return _context.Projectss.Any(e => e.ProjectsID == id);
        }
    }
}
