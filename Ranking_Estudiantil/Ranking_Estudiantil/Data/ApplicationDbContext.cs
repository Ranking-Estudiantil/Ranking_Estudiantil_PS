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
        public DbSet<Projects> Projectss { get; set; }
        public DbSet<Sanctions> Sanctionss { get; set; }
        public DbSet<PersonStudent> personStudents { get; set; }
        public DbSet<PersonProfessor> PersonProfessors { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("workstation id=RankingDB.mssql.somee.com;packet size=4096;user id=Nekotina387_SQLLogin_4;pwd=42ahq7uu97;data source=RankingDB.mssql.somee.com;initial catalog=RankingDB");
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
            modelBuilder.Entity<Projects>().ToTable("Projects");
            modelBuilder.Entity<Sanctions>().ToTable("Sanctions");
        }
        public Person ValidarUsuario(string _correo, string _password)
        {

             Person user= People.Where(item => item.Email == _correo && item.Password == _password).FirstOrDefault();
           

            return user;
        }
        public DbSet<Ranking_Estudiantil.Models.PersonStudent> PersonStudent { get; set; }
        public DbSet<Ranking_Estudiantil.Models.PersonProfessor> PersonProfessor { get; set; }
       
    }
}