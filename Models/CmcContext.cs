using Microsoft.EntityFrameworkCore;

namespace CMCS_Prototype.Models
{
    // CmcContext now inherits from DbContext and is the gateway to the database.
    public class CmcContext : DbContext
    {
        // The constructor takes the DbContextOptions to configure the database connection.
        public CmcContext(DbContextOptions<CmcContext> options) : base(options)
        {
        }

        // This DbSet represents the collection of Claim objects in the database.
        public DbSet<Claim> Claims { get; set; }

        // This DbSet represents the collection of User objects.
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed a default user for testing.
            // In a real application, you would manage user creation securely.
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Lecturer1", Password = "password123" },
                new User { Id = 2, Username = "Admin", Password = "adminpassword" }
            );
        }
    }
}
