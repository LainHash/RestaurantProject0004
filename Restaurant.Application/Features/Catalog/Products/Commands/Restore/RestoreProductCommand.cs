using MediatR;
using Restaurant.Application.Common.Models;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Restore
{
    public class RestoreProductCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public RestoreProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
