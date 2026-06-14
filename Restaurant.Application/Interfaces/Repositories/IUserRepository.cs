using Restaurant.Domain.Entities.Identity;

namespace Restaurant.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task<bool> UserExistsAsync(string userName);
    }
}
