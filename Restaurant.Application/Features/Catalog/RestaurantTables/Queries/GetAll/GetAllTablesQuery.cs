using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetAll
{
    public class GetAllTablesQuery : IRequest<Result<List<RestaurantTableDTO>>>
    {
    }
}
