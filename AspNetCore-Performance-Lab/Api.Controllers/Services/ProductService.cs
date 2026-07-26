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
        public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync()
        {
            var results = await _repository.GetAllAsync();
            return results.Select(MapToReadDto);
        }
        public async Task<ProductReadDto?> GetProductByIdAsync(int id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result != null ? MapToReadDto(result) : null;
        }
        public async Task<ProductReadDto> AddProductAsync(ProductCreateDto product)
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
            await _repository.AddAsync(productEntity);
            return MapToReadDto(productEntity);
        }
        public async Task<IEnumerable<ProductReadDto>> GetByCategoryAsync(string category)
        {
            var results= await _repository.GetByCategoryAsync(category);
            return results.Select(MapToReadDto);
        }
        public async Task<IEnumerable<ProductReadDto>> GetExpensiveProductsAsync(decimal price)
        {
            var results = await _repository.GetExpensiveProductsAsync(price);
            return results.Select(MapToReadDto);
        }
        public async Task<ProductReadDto?> SearchByNameAsync(string name)
        {
            var results= await _repository.SearchByNameAsync(name);
            return results != null ? MapToReadDto(results) : null;
        }
        public static  ProductReadDto MapToReadDto(Product product)
        {
            return new ProductReadDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Category = product.Category,
            };
        }
    }
}
