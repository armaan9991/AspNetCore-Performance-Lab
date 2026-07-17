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
    }
}
