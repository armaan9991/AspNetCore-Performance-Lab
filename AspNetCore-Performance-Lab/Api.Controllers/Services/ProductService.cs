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
        public IEnumerable<Product> GetAllProducts()
        {
            return _repository.GetAll();
        }
        public Product? GetProductById(int id)
        {
            return _repository.GetById(id);
        }
        public Product AddProduct(Product product)
        {
            return _repository.Add(product);
        }
    }
}
