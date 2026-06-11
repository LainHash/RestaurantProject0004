using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetOneByNumber
{
    public class GetOneTableByNumberQuery : IRequest<Result<RestaurantTableDTO>>
    {
        public int Floor {  get; set; }
        public int Number {  get; set; }
        public GetOneTableByNumberQuery(int floor, int number)
        {
            Floor = floor;
            Number = number;
        }
    }
}
