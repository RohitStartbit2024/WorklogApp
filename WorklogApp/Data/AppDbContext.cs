using Microsoft.EntityFrameworkCore;
using WorklogApp.Models;

namespace WorklogApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Worklog> Worklogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { Id = 1, Role = "Admin" },
                new UserRole { Id = 2, Role = "Manager" },
                new UserRole { Id = 3, Role = "Employee" }
            );
        }
    }

}
