using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Create
{
    public class CreateRestaurantTableCommand : IRequest<Result<RestaurantTableDTO>>
    {
        public CreateRestaurantTableDTO CreateRestaurantTableDTO { get; set; }
        public CreateRestaurantTableCommand(CreateRestaurantTableDTO createRestaurantTableDTO)
        {
            CreateRestaurantTableDTO = createRestaurantTableDTO;
        }
    }
}
