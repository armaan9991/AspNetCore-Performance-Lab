using Shared.Models;
namespace Api.Controllers.Services;

// Defines the business operations 
// used so we can loosly bound it to service so incase for testing we can just swap the service class with other 
// without breaking the code.
public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(int id);
    Task<Product> AddProductAsync(Product product);

}
