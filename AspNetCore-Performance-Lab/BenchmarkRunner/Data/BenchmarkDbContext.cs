using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace BenchmarkRunner.Data
{
    public class BenchmarkDbContext:DbContext
    {
        public BenchmarkDbContext(DbContextOptions<BenchmarkDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } 
    }
}
