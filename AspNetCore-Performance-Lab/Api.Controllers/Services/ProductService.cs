using Api.Controllers.Repositories.Interfaces;
using Shared.Models;

namespace Api.Controllers.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _repository.GetAllAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        public async Task<Product> AddProductAsync(Product product)
        {
            return await _repository.AddAsync(product);
        }
    }
}
