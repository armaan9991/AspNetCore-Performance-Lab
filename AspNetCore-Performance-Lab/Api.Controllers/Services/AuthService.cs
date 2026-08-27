using Api.Controllers.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Shared.DTOs;
using Shared.Models;
using Api.Controllers.Exceptions;

namespace Api.Controllers.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRespository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IJwttokenGenerator _jwtTokenGenerator;
        public AuthService(IUserRepository userRespository, PasswordHasher<User> passwordHasher, IJwttokenGenerator jwttokenGenerator)
        {
            _userRespository = userRespository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwttokenGenerator;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var emailExists = await _userRespository.EmailExistsAsync(registerDto.Email);
            if (emailExists)
            {
                throw new UserAlreadyExistsException("User with this email is already present.");
                //throw new Exception("Email is alreadty Exists");
            }
            var user = new User
            {
                Email = registerDto.Email,
                Role = "User",
                CreatedAt = DateTime.UtcNow
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

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                Role = user.Role,
                Token = token
            };

            //throw new NotImplementedException();
        }
    }
}
