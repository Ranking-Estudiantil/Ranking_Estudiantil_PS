using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ex_filtrado.Data;
using Ex_filtrado.Models;

namespace Ex_filtrado.Controllers
{
    public class EditModel : PageModel
    {
        private readonly Ex_filtrado.Data.StoreContext _context;

        public EditModel(Ex_filtrado.Data.StoreContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Person Person { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person =  await _context.Person.FirstOrDefaultAsync(m => m.PersonID == id);
            if (person == null)
            {
                return NotFound();
            }
            Person = person;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(Person.PersonID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PersonExists(int id)
        {
          return _context.Person.Any(e => e.PersonID == id);
        }
    }
}
