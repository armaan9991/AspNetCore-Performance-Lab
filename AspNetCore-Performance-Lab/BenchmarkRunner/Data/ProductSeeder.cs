using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace BenchmarkRunner.Data
{
    public class ProductSeeder
    {
        private readonly BenchmarkDbContext _context;

        public ProductSeeder(BenchmarkDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync(int count)
        {
            Console.WriteLine("current products" + await _context.Products.CountAsync());

            var products = new List<Product>();
            for(int i=1; i<= count; i++)
            {
                products.Add(new Product
                {
                    Name = $"Benchmark Product {i}",
                    Price = 100 + (i % 500),
                    Category = $"Category {(i % 10) + 1}"
                });
            }

            Console.WriteLine($"Generated {products.Count} products.");

            await _context.Products.AddRangeAsync(products);  // means add entire collection to context instead of addasync() .so we could add all instead only one.
            await _context.SaveChangesAsync();

            Console.WriteLine($"inserted {count} products.");
        }
    }
}
