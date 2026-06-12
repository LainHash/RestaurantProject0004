using MediatR;
using Restaurant.Application.Common.Models;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Delete
{
    public class DeleteProductCommand : IRequest<Result>
    {
        public Guid Id {  get; set; }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
