using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
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
        string pass;
        string user;
        string email;
        Random random = new Random();
        public PersonProfessorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PersonProfessors
        public async Task<IActionResult> Index()
        {
            IQueryable<Person> student = from Person in _context.People.Include(a => a.professor).Include(c =>c.career).Include(a => a.academicUnity)
                                        join Career in _context.Careers on Person.CareerID equals Career.CareerID
                                        join AcademicUnity in _context.AcademicUnities on Person.AcademicUnityID equals AcademicUnity.AcademicUnityID
                                         where  Person.Role != "Estudiante"
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
                .Include(p => p.academicUnity)
                .Include(p => p.career)
                .FirstOrDefaultAsync(m => m.PersonID == id);
            if (personProfessor == null)
            {
                return NotFound();
            }

            return View(personProfessor);
        }
        public void sendMail()
        {
            //Send Email-----------------------------------------------------------------------------
            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("devinamurrioUnivalle@gmail.com", "Kyocode", System.Text.Encoding.UTF8);//Correo de salida
            correo.To.Add(email); //Correo destino?
            correo.Subject = "Datos de Login"; //Asunto
            correo.Body = "Contrasenia: " + pass + "-Nombre de usuario: " + user; //Mensaje del correo
            correo.IsBodyHtml = true;
            correo.Priority = MailPriority.Normal;
            SmtpClient smtp = new SmtpClient();
            smtp.UseDefaultCredentials = false;
            smtp.Host = "smtp.gmail.com"; //Host del servidor de correo
            smtp.Port = 587; //Puerto de salida
            smtp.Credentials = new NetworkCredential("devinamurrioUnivalle@gmail.com", "zpksehcoeyqqqsol");//Cuenta de correo
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            smtp.EnableSsl = true;//True si el servidor de correo permite ssl
            smtp.Send(correo);
        }

        // GET: PersonProfessors/Create
        public IActionResult Create()
        {
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName");
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName");
            return View();
        }

        // POST: PersonProfessors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonID,FirstName,LastName,SecondLastName,AcademicUnityID,CareerID,Status,RegisterDate,Role,Email,Username,Password,ProfessorID")] PersonProfessor personProfessor)
        {
            
               
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
                        if (secondLast == null)
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
                    userName += random.Next(0, 9);
                }
                personProfessor.Username = userName;
                string password = string.Empty;
                for (int i = 0; i < 7; i++)
                {
                    int pass = random.Next(0, 10);
                    password += pass.ToString();
                }
                personProfessor.Password = password;
                user = userName;
                pass = password;
                email = personProfessor.Email;
                using (var dbContextTransaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        Person p = new()
                        {
                            FirstName = personProfessor.FirstName,
                            LastName = personProfessor.LastName,
                            SecondLastName = personProfessor.SecondLastName,
                            AcademicUnityID = personProfessor.AcademicUnityID,
                            CareerID = personProfessor.CareerID,
                            
                            Status = 1,
                            RegisterDate = DateTime.Now,
                            Role = personProfessor.Role,
                            Email = personProfessor.Email,
                            Username = userName,
                            Password = password


                        };
                        _context.Add(p);
                        await _context.SaveChangesAsync();

                        Professor s = new()
                        {
                            PersonID = p.PersonID,

                        };
                        _context.Add(s);
                        Thread backgroundThread = new Thread(new ThreadStart(sendMail));
                        backgroundThread.Start();
                        await _context.SaveChangesAsync();
                        dbContextTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbContextTransaction.Rollback();
                    }
                }

                    return RedirectToAction(nameof(Index));
            
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", personProfessor.AcademicUnityID);
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
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", personProfessor.AcademicUnityID);
            ViewData["CareerID"] = new SelectList(_context.Careers, "CareerID", "CareerName", personProfessor.CareerID);
            return View(personProfessor);
        }

        // POST: PersonProfessors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("PersonID,FirstName,LastName,SecondLastName,AcademicUnityID,CareerID,Status,RegisterDate,Role,Email,Username,Password,ProfessorID")] PersonProfessor personProfessor)
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
            ViewData["AcademicUnityID"] = new SelectList(_context.AcademicUnities, "AcademicUnityID", "AcademicUnityName", personProfessor.AcademicUnityID);
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
                .Include(p => p.academicUnity)
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
