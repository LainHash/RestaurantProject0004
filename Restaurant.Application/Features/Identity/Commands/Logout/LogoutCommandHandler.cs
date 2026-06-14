using MediatR;
using Restaurant.Application.Interfaces.Services;

namespace Restaurant.Application.Features.Identity.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, bool>
    {
        private readonly IAuthenticationService _authenticationService;

        public LogoutCommandHandler(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _authenticationService.LogoutAsync(request.UserId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
