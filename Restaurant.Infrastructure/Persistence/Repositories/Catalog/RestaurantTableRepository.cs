using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;

namespace Restaurant.Infrastructure.Persistence.Repositories.Catalog
{
    public class RestaurantTableRepository : IRestaurantTableRepository
    {
        private readonly RestaurantDbContext _context;
        public RestaurantTableRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<RestaurantTableDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var tables = await _context.RestaurantTables
                .Select(t => new RestaurantTableDTO
                {
                    TableNumber = t.TableNumber,
                    FloorNumber = t.FloorNumber,
                    Shape = t.Shape,
                    Capacity = t.Capacity,
                    Status = t.Status,
                    Description = t.Description ?? string.Empty,
                })
                .ToListAsync(cancellationToken);
            return Result<List<RestaurantTableDTO>>.Success(tables, "Lấy danh sách Bàn thành công.");
        }
    }
}
