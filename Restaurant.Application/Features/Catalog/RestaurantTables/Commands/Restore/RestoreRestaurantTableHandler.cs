using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Restore
{
    public class RestoreRestaurantTableHandler : IRequestHandler<RestoreRestaurantTableCommand, Result>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public RestoreRestaurantTableHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<Result> Handle(RestoreRestaurantTableCommand request, CancellationToken cancellationToken)
        {
            var response = await _restaurantTableRepository.RestoreAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
