using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetAll
{
    public class GetAllTablesHandler : IRequestHandler<GetAllTablesQuery, Result<List<RestaurantTableDTO>>>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public GetAllTablesHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }
        public async Task<Result<List<RestaurantTableDTO>>> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var response = await _restaurantTableRepository
                .GetAllAsync(cancellationToken);
            return response;
        }
    }
}
