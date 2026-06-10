using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetAllByFloor
{
    public class GetAllByFloorQuery : IRequest<Result<List<RestaurantTableDTO>>>
    {
        public int Floor { get; set; }
        public GetAllByFloorQuery(int floor)
        {
            Floor = floor;
        }
    }
}
