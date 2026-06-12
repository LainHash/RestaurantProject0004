using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Update
{
    public class UpdateRestaurantTableHandler : IRequestHandler<UpdateRestaurantTableCommand, Result<RestaurantTableDTO>>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public UpdateRestaurantTableHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<Result<RestaurantTableDTO>> Handle(UpdateRestaurantTableCommand request, CancellationToken cancellationToken)
        {
            var response = await _restaurantTableRepository.UpdateAsync(request.Id, request.UpdateRestaurantTableDTO, cancellationToken);
            return response;
        }
    }
}
