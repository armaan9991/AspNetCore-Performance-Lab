using Shared.Models;

namespace Api.Controllers.Repositories.Interfaces
{
    public interface IUserRespository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<bool> EmailExistsAsync(string email);
    }
}
