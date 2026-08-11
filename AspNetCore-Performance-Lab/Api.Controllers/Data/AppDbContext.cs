using Microsoft.EntityFrameworkCore;
using Shared.Models;
namespace Api.Controllers.Data
{
    public class AppDbContext : DbContext   // inherit all EF core DbContext capabilites
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)   // DBcontextoptions tells which database, connection string 
        {

        }
        public DbSet<Product> Products { get; set; }   // products table
        public DbSet<User> Users { get; set; }   // users table 

        // Make Each users email Unique
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // before doing custom configuration, we call base.OnModelCreating to ensure
            // that any default configurations are applied first.
            base.OnModelCreating(modelBuilder);
            // we have to configure User entity and then we check create index for DB as email for al users and then make it unique .
            // so now two users cannot have same email address in the database.
            modelBuilder.Entity<User>().
                HasIndex(u => u.Email).IsUnique();
        }
    }
}
