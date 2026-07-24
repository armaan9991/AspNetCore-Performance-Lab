using Api.Controllers.Repositories.Interfaces;
using Shared.DTOs;
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
        public async Task<Product> AddProductAsync(ProductCreateDto product)
        {
            if (product.Price<0)
            {
                throw new Exception("no negative price");
            }
            var item = await _repository.SearchByNameAsync(product.Name);
            if (item != null)
            {
                throw new Exception("Already present!!");
            }
            var productEntity = new Product
            {
                Name = product.Name,
                Price = product.Price,
                Category = product.Category
            };
            return await _repository.AddAsync(productEntity);
        }
        public async Task<IEnumerable<Product>> GetByCategoryAsync(string category)
        {
            return await _repository.GetByCategoryAsync(category);
        }
        public async Task<IEnumerable<Product>> GetExpensiveProductsAsync(decimal price)
        {
            return await _repository.GetExpensiveProductsAsync(price);
        }
        public async Task<Product?> SearchByNameAsync(string name)
        {
            return await _repository.SearchByNameAsync(name);
        }
    }
}
