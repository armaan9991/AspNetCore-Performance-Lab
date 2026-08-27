using Shared.Models;
using Api.Controllers.Repositories.Interfaces;
using Api.Controllers.Data;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs;

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

        public async Task<Product?> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var prod = await _context.Products.FindAsync(id);
            
            if(prod == null)
            {
                return false;
            }
            _context.Products.Remove(prod);
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<PagedResponseDto<Product>> GetPagedAsync(ProductQueryDto query)
        {
            // BUILd a IQuerable query.

            var products = _context.Products.AsNoTracking().AsQueryable();
            

            if (!string.IsNullOrWhiteSpace(query.Category))
            {
                products = products.Where(p => p.Category == query.Category);
            }
            if (!string.IsNullOrWhiteSpace(query.Search))
            {
                products = products.Where(p => p.Name.Contains(query.Search));
            }
            if(query.SortBy?.ToLower() == "price")
            {
                products = query.SortDescending ? products.OrderByDescending(p => p.Name)
            : products.OrderBy(p => p.Name);
            }
            else
            {
                products = products.OrderBy(p => p.Id);
            }
            var totalItems = await products.CountAsync();

            var items = await products.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToListAsync();

            var pagedQ = products.Skip((query.Page - 1) * query.PageSize).Take(query.PageSize).ToQueryString();
            Console.WriteLine(pagedQ);


            var totalPages = (int)Math.Ceiling(totalItems / (double)query.PageSize);

            return new PagedResponseDto<Product>
            {
                Items = items,
                Page = query.Page,
                PageSize = query.PageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                HasNextPage = query.Page < totalPages,
                HasPreviousPage = query.Page > 1,
            };
        }

    }
}
