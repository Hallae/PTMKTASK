using Microsoft.EntityFrameworkCore;
namespace PTMKTASK.Models
{
    public class EmployeeContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=PTMKdb;Username=postgres;Port=5432;password=02190");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
               .HasKey(e => e.Id);
            modelBuilder.Entity<Employee>()
               .Property(e => e.DateOfBirth).HasColumnType("DATE"); // Set the type of DateOfBirth column
        }
    }
}
