using Shared.Models;
using Api.Controllers.Repositories.Interfaces;
using Api.Controllers.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        //public IEnumerable<Product> GetAll()
        //{
        //    return _products;
        //}

        // async makes requests non-blocking, allowing other operations
        // to continue while waiting for the database response
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
       }
        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            var result = _context.Products.AsNoTracking()
                .Where(p => p.Category == category);
            return await result.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetExpensiveProductsAsync(decimal price)
        {
            var result = _context.Products.AsNoTracking().
                Where(p => p.Price >= price);
            return await result.ToListAsync();
        }
        public async Task<Product?> SearchByNameAsync(string name)
        {
            var result = await _context.Products.AsNoTracking().
                FirstOrDefaultAsync(p => p.Name == name);
            return result;
        }
    }
}
