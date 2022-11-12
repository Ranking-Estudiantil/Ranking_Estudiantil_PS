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
    public class IndexModel : PageModel
    {
        private readonly Ex_filtrado.Data.StoreContext _context;

        public IndexModel(Ex_filtrado.Data.StoreContext context)
        {
            _context = context;
        }

        public IList<Person> Person { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Person != null)
            {
                Person = await _context.Person.ToListAsync();
            }
        }
    }
}
