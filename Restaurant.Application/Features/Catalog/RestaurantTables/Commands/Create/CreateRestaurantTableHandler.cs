using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Create
{
    public class CreateRestaurantTableHandler : IRequestHandler<CreateRestaurantTableCommand, Result<RestaurantTableDTO>>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public CreateRestaurantTableHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<Result<RestaurantTableDTO>> Handle(CreateRestaurantTableCommand request, CancellationToken cancellationToken)
        {
            var response = await _restaurantTableRepository.CreateAsync(request.CreateRestaurantTableDTO, cancellationToken);
            return response;
        }
    }
}
