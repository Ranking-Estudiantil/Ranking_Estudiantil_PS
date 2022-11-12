using Ex_filtrado.Models;
using Microsoft.EntityFrameworkCore;

namespace Ex_filtrado.Data
{
    public class StoreContext: DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }
        public DbSet<Person> Person { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");

        }

    }
}
