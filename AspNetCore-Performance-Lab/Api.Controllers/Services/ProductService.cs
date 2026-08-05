using Api.Controllers.Repositories.Interfaces;
using Shared.DTOs;
using Shared.Models;
using Api.Controllers.Exceptions;
using Microsoft.Extensions.Options;
using Api.Controllers.Settings;

namespace Api.Controllers.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly AppSettings _settings;
        public ProductService(IProductRepository repository,IOptions<AppSettings> options)
        {
            _repository = repository;
            _settings = options.Value;
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
                throw new InvalidPriceException("no negative price");
            }
            var item = await _repository.SearchByNameAsync(product.Name);
            if (item != null)
            {
                //throw new Exception("Already present!!");
                throw new ProductAlreadyExistsException($"Product with name {product.Name} already exists.");
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
        public async Task<ProductReadDto?> UpdateProductAsync(int id,ProductUpdateDto productUpdateDto)
        {
            var prod = await _repository.GetByIdAsync(id);
            if(prod == null)
            {
                return null;
            }
            prod.Name = productUpdateDto.Name;
            prod.Price = productUpdateDto.Price;
            prod.Category = productUpdateDto.Category;

            await _repository.UpdateAsync(prod);
            return MapToReadDto(prod);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            return await _repository.DeleteAsync(id);
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
