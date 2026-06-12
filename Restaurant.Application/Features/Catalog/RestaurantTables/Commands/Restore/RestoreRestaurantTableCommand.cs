using MediatR;
using Restaurant.Application.Common.Models;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Restore
{
    public class RestoreRestaurantTableCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public RestoreRestaurantTableCommand(Guid id)
        {
            Id = id;
        }
    }
}
