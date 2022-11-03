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
        public DbSet<Professor> Professors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Gameficacion2;User Id=sa;password=Univalle;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicUnity>().ToTable("AcademicUnity");
            modelBuilder.Entity<Career>().ToTable("Career");
            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Faculty>().ToTable("Faculty");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Professor>().ToTable("Professor");

        }
        public Person ValidarUsuario(string _correo, string _password)
        {

            return People.Where(item => item.Email == _correo && item.Password == _password).FirstOrDefault();
        }
    }
}