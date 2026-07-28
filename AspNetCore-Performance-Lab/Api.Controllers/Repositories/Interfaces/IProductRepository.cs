using Shared.DTOs;
using Shared.Models;

namespace Api.Controllers.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);

        Task<IEnumerable<Product>> GetExpensiveProductsAsync(decimal price);

        Task<Product?> SearchByNameAsync(string name);
    }
}
