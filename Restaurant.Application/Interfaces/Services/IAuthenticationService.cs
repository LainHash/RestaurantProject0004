using Restaurant.Application.Common.DTOs;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IAuthenticationService
    {
        Task<AuthResponseDto> LoginAsync(string userName, string password);
        Task LogoutAsync(int userId);
    }
}
