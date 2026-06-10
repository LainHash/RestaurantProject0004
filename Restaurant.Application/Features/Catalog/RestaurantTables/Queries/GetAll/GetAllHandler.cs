using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetAll
{
    public class GetAllHandler : IRequestHandler<GetAllQuery, Result<List<RestaurantTableDTO>>>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public GetAllHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }
        public async Task<Result<List<RestaurantTableDTO>>> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            var result = await _restaurantTableRepository.GetAllAsync(cancellationToken);
            return result;
        }
    }
}
