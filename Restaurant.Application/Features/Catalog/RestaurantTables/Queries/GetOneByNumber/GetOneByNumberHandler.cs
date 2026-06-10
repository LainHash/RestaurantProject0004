using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetOneByNumber
{
    public class GetOneByNumberHandler : IRequestHandler<GetOneByNumberQuery, Result<RestaurantTableDTO>>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public GetOneByNumberHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<Result<RestaurantTableDTO>> Handle(GetOneByNumberQuery request, CancellationToken cancellationToken)
        {
            var result = await _restaurantTableRepository.GetOneByNumberAsync(request.Floor, request.Number, cancellationToken);
            return result;
        }
    }
}
