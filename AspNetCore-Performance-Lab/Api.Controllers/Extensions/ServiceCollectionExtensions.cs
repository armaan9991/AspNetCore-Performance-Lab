using Api.Controllers.Repositories.Implementations;
using Api.Controllers.Repositories.Interfaces;
using Api.Controllers.Services;

namespace Api.Controllers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
