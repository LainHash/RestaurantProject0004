using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using System.Net;

namespace Restaurant.Infrastructure.Persistence.Repositories.Catalog
{
    public class RestaurantTableRepository : IRestaurantTableRepository
    {
        private readonly RestaurantDbContext _context;
        public RestaurantTableRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<RestaurantTableDTO>>> 
            GetAllAsync(CancellationToken cancellationToken)
        {
            var tables = await _context.RestaurantTables
                .Select(tb => new RestaurantTableDTO(tb))
                .ToListAsync(cancellationToken);

            return Result<List<RestaurantTableDTO>>
                .Success(tables, "Lấy danh sách Bàn thành công.");
        }

        public async Task<Result<List<RestaurantTableDTO>>> 
            GetAllByFloorAsync(int floor, CancellationToken cancellationToken)
        {
            var tables = await _context.RestaurantTables
                .Where(t => t.FloorNumber == floor)
                .Select(tb => new RestaurantTableDTO(tb))
                .ToListAsync(cancellationToken);

            return Result<List<RestaurantTableDTO>>
                .Success(tables, "Lấy danh sách Bàn theo Tầng thành công.");
        }

        public async Task<Result<RestaurantTableDTO>> 
            GetOneByNumberAsync(int floor, int number, CancellationToken cancellationToken)
        {
            var table = await _context.RestaurantTables
                .Where(t => t.FloorNumber == floor && t.TableNumber == number)
                .Select(tb => new RestaurantTableDTO(tb))
                .FirstOrDefaultAsync(cancellationToken);

            if(table == null)
            {
                return Result<RestaurantTableDTO>
                    .Fail("Bàn không tồn tại", HttpStatusCode.NotFound);
            }

            return Result<RestaurantTableDTO>
                .Success(table, "Lấy bàn theo Số thành công.");
        }
    }
}
