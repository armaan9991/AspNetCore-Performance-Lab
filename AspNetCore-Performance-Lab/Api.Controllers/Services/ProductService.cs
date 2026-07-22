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
            if (product.Price<0)
            {
                throw new Exception("no negative price");
            }
            var item = await _repository.GetByIdAsync(product.Id);
            if(item != null)
            {
                throw new Exception("Already present!!");
            }

            return await _repository.AddAsync(product);
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
