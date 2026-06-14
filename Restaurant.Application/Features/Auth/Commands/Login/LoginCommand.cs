using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Auth.DTOs;

namespace Restaurant.Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<Result>
    {
        public LoginDTO LoginDTO { get; set; } = null!;
        public LoginCommand(LoginDTO loginDTO)
        {
            LoginDTO = loginDTO;
        }
    }
}
