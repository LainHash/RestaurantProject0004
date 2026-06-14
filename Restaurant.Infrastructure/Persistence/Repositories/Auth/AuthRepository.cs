using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Enums;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Auth.DTOs;
using Restaurant.Application.Interfaces.Repositories.Auth;
using System.Net;

namespace Restaurant.Infrastructure.Persistence.Repositories.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly RestaurantDbContext _context;
        public AuthRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Result> LoginAsync(LoginDTO request, CancellationToken cancellationToken)
        {
            var user = await _context.Users
                .Where(u => u.Email == request.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null || BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) 
            {
                return Result
                    .Fail("Sai mật khẩu hoặc Email.", HttpStatusCode.Unauthorized);
            }

            if(user.Status == nameof(UserStatus.Inactive))
            {
                return Result
                    .Fail("Tài khoản chưa được xác thực.", HttpStatusCode.Forbidden);
            }

            return Result
                .Success("Đăng nhập thành công.", HttpStatusCode.OK);
        }
    }
}
