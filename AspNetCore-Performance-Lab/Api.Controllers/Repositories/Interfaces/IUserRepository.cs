using Shared.Models;

namespace Api.Controllers.Repositories.Interfaces
{
    // DEFINES the contract for user repository operations,
    // allowing for loose coupling and easier testing.
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<User> AddAsync(User user);
        Task<bool> EmailExistsAsync(string email);
    }
}
