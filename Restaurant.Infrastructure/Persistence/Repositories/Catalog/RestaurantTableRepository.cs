using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using Restaurant.Domain.Entities.Catalog;
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

            if (table == null)
            {
                return Result<RestaurantTableDTO>
                    .Fail("Bàn không tồn tại", HttpStatusCode.NotFound);
            }

            return Result<RestaurantTableDTO>
                .Success(table, "Lấy bàn theo Số thành công.");
        }

        public async Task<Result<RestaurantTableDTO>>
            CreateAsync(CreateRestaurantTableDTO request, CancellationToken cancellationToken)
        {
            if (await _context.RestaurantTables
                .AnyAsync(t => t.FloorNumber == request.FloorNumber && t.TableNumber == request.TableNumber,
                          cancellationToken))
            {
                return Result<RestaurantTableDTO>
                    .Fail($"Bàn số {request.TableNumber} tầng {request.FloorNumber} đã tồn tại.", HttpStatusCode.Conflict);
            }

            var table = new RestaurantTable(
                request.TableNumber,
                request.FloorNumber,
                request.Capacity,
                request.Shape,
                request.Status,
                request.Description);

            await _context.RestaurantTables.AddAsync(table, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var dto = new RestaurantTableDTO(table);

            return Result<RestaurantTableDTO>
                .Success(dto, "Thêm Bàn thành công.", HttpStatusCode.Created);
        }

        public async Task<Result<RestaurantTableDTO>>
            UpdateAsync(Guid id, UpdateRestaurantTableDTO request, CancellationToken cancellationToken)
        {
            var table = await _context.RestaurantTables
                .Where(t => t.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (table == null)
            {
                return Result<RestaurantTableDTO>
                    .Fail("Bàn không tồn tại.", HttpStatusCode.NotFound);
            }

            table.Update(
                request.TableNumber,
                request.FloorNumber,
                request.Capacity,
                request.Shape,
                request.Status,
                request.Description);

            await _context.SaveChangesAsync(cancellationToken);

            var dto = new RestaurantTableDTO(table);

            return Result<RestaurantTableDTO>
                .Success(dto, "Cập nhật Bàn thành công.", HttpStatusCode.OK);
        }

        public async Task<Result>
            DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var table = await _context.RestaurantTables
                .Where(t => t.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (table == null)
            {
                return Result
                    .Fail("Bàn không tồn tại.", HttpStatusCode.NotFound);
            }

            if (table.IsDeleted)
            {
                return Result
                    .Fail("Bàn đã bị xóa.", HttpStatusCode.Conflict);
            }

            table.SoftDelete();

            await _context.SaveChangesAsync(cancellationToken);

            return Result
                .Success("Xóa Bàn thành công.", HttpStatusCode.OK);
        }

        public async Task<Result>
            RestoreAsync(Guid id, CancellationToken cancellationToken)
        {
            var table = await _context.RestaurantTables
                .Where(t => t.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (table == null)
            {
                return Result
                    .Fail("Bàn không tồn tại.", HttpStatusCode.NotFound);
            }

            if (!table.IsDeleted)
            {
                return Result
                    .Fail("Bàn chưa bị xóa.", HttpStatusCode.Conflict);
            }

            table.Restore();

            await _context.SaveChangesAsync(cancellationToken);

            return Result
                .Success("Khôi phục Bàn thành công.", HttpStatusCode.OK);
        }
    }
}
