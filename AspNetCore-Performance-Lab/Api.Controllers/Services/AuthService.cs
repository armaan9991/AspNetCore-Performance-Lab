using Api.Controllers.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using Shared.Models;

namespace Api.Controllers.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRespository _userRespository;
        private readonly PasswordHasher<User> _passwordHasher;
        public AuthService(IUserRespository userRespository, PasswordHasher<User> passwordHasher)
        {
            _userRespository = userRespository;
            _passwordHasher = passwordHasher;
        }

        public Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            throw new NotImplementedException();
        }
        public Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }
    }
}
