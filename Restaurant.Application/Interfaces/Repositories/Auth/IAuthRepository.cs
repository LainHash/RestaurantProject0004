using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Auth.DTOs;

namespace Restaurant.Application.Interfaces.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<Result> LoginAsync(LoginDTO request, CancellationToken cancellationToken);
    }
}
