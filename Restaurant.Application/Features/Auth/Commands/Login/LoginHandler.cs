using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Interfaces.Repositories.Auth;

namespace Restaurant.Application.Features.Auth.Commands.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, Result>
    {
        private readonly IAuthRepository _authRepository;
        public LoginHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public async Task<Result> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = await _authRepository.LoginAsync(request.LoginDTO, cancellationToken);
            return response;
        }
    }
}
