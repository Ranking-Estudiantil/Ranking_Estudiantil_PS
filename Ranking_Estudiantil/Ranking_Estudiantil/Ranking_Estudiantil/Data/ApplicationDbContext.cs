using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ranking_Estudiantil.Models;

namespace Ranking_Estudiantil.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public  DbSet<AcademicUnity> AcademicUnities { get; set; }
       
        public  DbSet<Career> Careers { get; set; }
        public  DbSet<Department> Departments { get; set; } 
        public  DbSet<Faculty> Faculties { get; set; } 
        public  DbSet<Student> Students { get; set; }
        public DbSet<Person> People { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicUnity>().ToTable("AcademicUnity");
            modelBuilder.Entity<Career>().ToTable("Career");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Faculty>().ToTable("Faculty");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Person>().ToTable("Person");

        }
        public Person ValidarUsuario(string _correo, string _password)
        {

            return People.Where(item => item.Email == _correo && item.Password == _password).FirstOrDefault();
        }
    }
}