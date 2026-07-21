using Shared.Models;

namespace Api.Controllers.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddAsync(Product product);
        Task<IEnumerable<Product>> GetByCategoryAsync(string category);

        Task<IEnumerable<Product>> GetExpensiveProductsAsync(decimal price);

        Task<Product?> SearchByNameAsync(string name);
    }
}
