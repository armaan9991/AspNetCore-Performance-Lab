using Api.Controllers.Repositories.Implementations;
using Api.Controllers.Repositories.Interfaces;
using Api.Controllers.Services;
using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace Api.Controllers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<PasswordHasher<User>>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}