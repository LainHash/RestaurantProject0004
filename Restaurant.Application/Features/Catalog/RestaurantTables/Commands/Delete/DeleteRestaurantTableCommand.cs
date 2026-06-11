using MediatR;
using Restaurant.Application.Common.Models;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Delete
{
    public class DeleteRestaurantTableCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public DeleteRestaurantTableCommand(Guid id)
        {
            Id = id;
        }
    }
}
