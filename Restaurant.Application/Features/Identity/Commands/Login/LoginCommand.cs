using MediatR;
using Restaurant.Application.Common.DTOs;

namespace Restaurant.Application.Features.Identity.Commands.Login
{
    public class LoginCommand : IRequest<AuthResponseDto>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
