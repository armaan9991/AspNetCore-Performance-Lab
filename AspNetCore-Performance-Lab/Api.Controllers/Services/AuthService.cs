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

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var emailExists = await _userRespository.EmailExistsAsync(registerDto.Email);
            if (emailExists)
            {
                throw new Exception("Email is alreadty Exists");
            }
            var user = new User
            {
                Email = registerDto.Email,
                Role = "User",
                CreatedAt = DateTime.Now
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);

            await _userRespository.AddAsync(user);
            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            };
        }
        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRespository.GetByEmailAsync(loginDto.Email);
            if (user == null) { 
                    return null;
            }
            // gives failed or success
            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            
            if(result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role
            };

            //throw new NotImplementedException();
        }
    }
}
