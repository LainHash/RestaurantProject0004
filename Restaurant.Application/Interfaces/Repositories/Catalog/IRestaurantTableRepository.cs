using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;

namespace Restaurant.Application.Interfaces.Repositories.Catalog
{
    public interface IRestaurantTableRepository
    {
        Task<Result<List<RestaurantTableDTO>>> GetAllAsync(CancellationToken cancellationToken);
    }
}
