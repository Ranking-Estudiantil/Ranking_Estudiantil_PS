using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Ex_filtrado.Data;
using Ex_filtrado.Models;

namespace Ex_filtrado.Controllers
{
    public class DeleteModel : PageModel
    {
        private readonly Ex_filtrado.Data.StoreContext _context;

        public DeleteModel(Ex_filtrado.Data.StoreContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Person Person { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FirstOrDefaultAsync(m => m.PersonID == id);

            if (person == null)
            {
                return NotFound();
            }
            else 
            {
                Person = person;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }
            var person = await _context.Person.FindAsync(id);

            if (person != null)
            {
                Person = person;
                _context.Person.Remove(Person);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
