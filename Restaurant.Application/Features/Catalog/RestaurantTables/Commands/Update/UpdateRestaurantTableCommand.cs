using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Update
{
    public class UpdateRestaurantTableCommand : IRequest<Result<RestaurantTableDTO>>
    {
        public Guid Id { get; set; }
        public UpdateRestaurantTableDTO UpdateRestaurantTableDTO { get; set; } = null!;
        public UpdateRestaurantTableCommand(Guid id, UpdateRestaurantTableDTO updateRestaurantTableDTO)
        {
            Id = id;
            UpdateRestaurantTableDTO = updateRestaurantTableDTO;
        }
    }
}
