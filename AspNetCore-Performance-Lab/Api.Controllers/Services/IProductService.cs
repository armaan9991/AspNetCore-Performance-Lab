using Shared.Models;
using Shared.DTOs;
namespace Api.Controllers.Services;
// Defines the business operations 
// used so we can loosly bound it to service so incase for testing we can just swap the service class with other 
// without breaking the code.
public interface IProductService
{
    Task<IEnumerable<ProductReadDto>> GetAllProductsAsync();
    Task<ProductReadDto?> GetProductByIdAsync(int id);
    Task<ProductReadDto> AddProductAsync(ProductCreateDto product);
    Task<ProductReadDto?> UpdateProductAsync(int id, ProductUpdateDto product);
    Task<bool> DeleteProductAsync(int id);
    Task<IEnumerable<ProductReadDto>> GetByCategoryAsync(string category);
    Task<IEnumerable<ProductReadDto>> GetExpensiveProductsAsync(decimal price);
    Task<ProductReadDto?> SearchByNameAsync(string name);
} 