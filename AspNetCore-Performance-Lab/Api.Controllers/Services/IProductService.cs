using Shared.Models;
namespace Api.Controllers.Services;

// Defines the business operations 
// used so we can loosly bound it to service so incase for testing we can just swap the service class with other 
// without breaking the code.
public interface IProductService
{
    IEnumerable<Product> GetAllProducts();
    Product? GetProductById(int id);
    Product AddProduct(Product product);
}
