
using Microsoft.EntityFrameworkCore;
namespace achievments.Models
{
    public class AchieveContext: DbContext
    {
        public AchieveContext()
        {

        }
        public AchieveContext(DbContextOptions<AchieveContext> options)
            : base(options)
        {
        }
        public DbSet<Achievments> Achievments { get; set; }

        public DbSet<Registros> registros { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Achievments>().ToTable("Achievments");
            modelBuilder.Entity<Registros>().ToTable("Registros");
            

        }
    }
}
