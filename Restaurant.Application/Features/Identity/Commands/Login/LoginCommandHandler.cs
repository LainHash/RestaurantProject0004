using MediatR;
using Restaurant.Application.Common.DTOs;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Domain.Common.Exceptions;

namespace Restaurant.Application.Features.Identity.Commands.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IAuthenticationService _authenticationService;

        public LoginCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(request.UserName, request.Password);
                return result;
            }
            catch (AuthenticationException ex)
            {
                return new AuthResponseDto
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
