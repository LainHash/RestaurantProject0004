using Restaurant.Application.Common.DTOs;
using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Domain.Common.Exceptions;

namespace Restaurant.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHashService _passwordHashService;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationService(
            IUserRepository userRepository,
            IPasswordHashService passwordHashService,
            IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHashService = passwordHashService;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponseDto> LoginAsync(string userName, string password)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                throw new AuthenticationException("Invalid username or password");
            }

            if (!_passwordHashService.VerifyPassword(password, user.PasswordHash))
            {
                throw new AuthenticationException("Invalid username or password");
            }

            var token = _jwtTokenService.GenerateToken(user);

            return new AuthResponseDto
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleName = user.Role.Name
                }
            };
        }

        public async Task LogoutAsync(int userId)
        {
            // In a stateless JWT system, logout is typically handled client-side
            // by removing the token. You could implement token blacklisting here if needed.
            await Task.CompletedTask;
        }
    }
}
