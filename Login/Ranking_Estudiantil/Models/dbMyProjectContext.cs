using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Ranking_Estudiantil.Models
{
    public partial class dbMyProjectContext : DbContext
    {
        public dbMyProjectContext()
        {
        }

        public dbMyProjectContext(DbContextOptions<dbMyProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicUnity> AcademicUnities { get; set; } = null!;
        public virtual DbSet<Achievment> Achievments { get; set; } = null!;
        public virtual DbSet<Career> Careers { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Person> People { get; set; } = null!;
        public virtual DbSet<Professor> Professors { get; set; } = null!;
        public virtual DbSet<Rank> Ranks { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("workstation id=dbMyProject.mssql.somee.com;packet size=4096;user id=Nekotina365_SQLLogin_1;pwd=ebqahly7ck;data source=dbMyProject.mssql.somee.com;persist security info=False;initial catalog=dbMyProject");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcademicUnity>(entity =>
            {
                entity.ToTable("AcademicUnity");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Achievment>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("category");

                entity.Property(e => e.Description)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Career>(entity =>
            {
                entity.ToTable("Career");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdDepartment).HasColumnName("idDepartment");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdDepartmentNavigation)
                    .WithMany(p => p.Careers)
                    .HasForeignKey(d => d.IdDepartment)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Career_Department");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdFaculty).HasColumnName("idFaculty");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdFacultyNavigation)
                    .WithMany(p => p.Departments)
                    .HasForeignKey(d => d.IdFaculty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Department_Faculty");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdAcademicUnity).HasColumnName("idAcademicUnity");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdAcademicUnityNavigation)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.IdAcademicUnity)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Faculty_AcademicUnity");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.LastUpdate)
                    .HasColumnType("datetime")
                    .HasColumnName("lastUpdate");

                entity.Property(e => e.Password)
                    .HasMaxLength(25)
                    .HasColumnName("password");

                entity.Property(e => e.RegistreDate)
                    .HasColumnType("datetime")
                    .HasColumnName("registreDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Role)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("role");

                entity.Property(e => e.SecondLastName)
                    .HasMaxLength(45)
                    .IsUnicode(false)
                    .HasColumnName("secondLastName");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Professor>(entity =>
            {
                entity.ToTable("Professor");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.IdCareer).HasColumnName("idCareer");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Professor)
                    .HasForeignKey<Professor>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professor_Person");

                entity.HasOne(d => d.IdCareerNavigation)
                    .WithMany(p => p.Professors)
                    .HasForeignKey(d => d.IdCareer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Professor_Career");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("Rank");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.IdCareer).HasColumnName("idCareer");

                entity.Property(e => e.IdRank).HasColumnName("idRank");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Person");

                entity.HasOne(d => d.IdCareerNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.IdCareer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Career");

                entity.HasOne(d => d.IdRankNavigation)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.IdRank)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Rank");

                entity.HasMany(d => d.IdAchievments)
                    .WithMany(p => p.IdStudents)
                    .UsingEntity<Dictionary<string, object>>(
                        "AchievStudent",
                        l => l.HasOne<Achievment>().WithMany().HasForeignKey("IdAchievment").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Achiev_Student_Achievments"),
                        r => r.HasOne<Student>().WithMany().HasForeignKey("IdStudent").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Achiev_Student_Student"),
                        j =>
                        {
                            j.HasKey("IdStudent", "IdAchievment");

                            j.ToTable("Achiev_Student");

                            j.IndexerProperty<int>("IdStudent").HasColumnName("idStudent");

                            j.IndexerProperty<short>("IdAchievment").HasColumnName("idAchievment");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }
        public Person ValidarUsuario(string _correo, string _password)
        {

            return People.Where(item => item.Email == _correo && item.Password == _password).FirstOrDefault();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
