using Shared.Models;
using Api.Controllers.Repositories.Interfaces;
namespace Api.Controllers.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products =
        [
            new Product
            {
                Id =1,
                Name = "LapTop",
                Price = 1200,
                Category = "Electronics"
            },
            new Product
            {
                Id =2,
                Name = "Keyboard",
                Price = 80,
                Category = "Accessories"
            },
            new Product
            {
                Id =3,
                Name = "Mouse",
                Price = 50,
                Category = "Accessories"
            },
            new Product
            {
                Id =4,
                Name = "Monitor",
                Price = 300,
                Category = "Electronics"
            }
        ];

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }
        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }
        public Product Add(Product product)
        {
            _products.Add(product);
            return product;
        }
    }
}
