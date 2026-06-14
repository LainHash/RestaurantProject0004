using MediatR;

namespace Restaurant.Application.Features.Identity.Commands.Logout
{
    public class LogoutCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
