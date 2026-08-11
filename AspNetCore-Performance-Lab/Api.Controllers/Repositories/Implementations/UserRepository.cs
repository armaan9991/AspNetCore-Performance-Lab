using Api.Controllers.Data;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Controllers.Repositories.Implementations
{
    public class UserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User?>  GetByIdAsync (int id) {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
}
