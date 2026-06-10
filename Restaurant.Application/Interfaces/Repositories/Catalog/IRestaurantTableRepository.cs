using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;

namespace Restaurant.Application.Interfaces.Repositories.Catalog
{
    public interface IRestaurantTableRepository
    {
        Task<Result<List<RestaurantTableDTO>>> 
            GetAllAsync(CancellationToken cancellationToken);

        Task<Result<List<RestaurantTableDTO>>> 
            GetAllByFloorAsync(int floor, CancellationToken cancellationToken);

        Task<Result<RestaurantTableDTO>>
            GetOneByNumberAsync(int floor, int number, CancellationToken cancellationToken);
    }
}
