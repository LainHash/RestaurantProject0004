using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(User user);
    }
}
