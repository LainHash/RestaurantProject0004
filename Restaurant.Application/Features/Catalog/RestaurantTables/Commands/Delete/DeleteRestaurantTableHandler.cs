using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Delete
{
    public class DeleteRestaurantTableHandler : IRequestHandler<DeleteRestaurantTableCommand, Result>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public DeleteRestaurantTableHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<Result> Handle(DeleteRestaurantTableCommand request, CancellationToken cancellationToken)
        {
            var response = await _restaurantTableRepository.DeleteAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
