using Microsoft.EntityFrameworkCore;

namespace Assignment.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed initial data
            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Title = "Math 101", Description = "Basic Mathematics", Credits = 3 },
                new Course { Id = 2, Title = "Physics 101", Description = "Introduction to Physics", Credits = 4 }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Winifred Rex", Email = "rex@baze.com", Age = 20 },
                new Student { Id = 2, Name = "Mohammed Muktar", Email = "muktar@werey.com", Age = 22 }
            );
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=Assignment;Database=AssignmentDb;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }
    }
}
