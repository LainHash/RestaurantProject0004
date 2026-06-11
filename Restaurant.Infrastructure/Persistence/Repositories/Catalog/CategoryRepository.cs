using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using Restaurant.Domain.Entities.Catalog;
using System.Net;

namespace Restaurant.Infrastructure.Persistence.Repositories.Catalog
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly RestaurantDbContext _context;
        public CategoryRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CategoryDTO>>> 
            GetAllAsync(CancellationToken cancellationToken)
        {
            var categories = await _context.Categories
                .Select(cat => new CategoryDTO(cat))
                .ToListAsync(cancellationToken);
            return Result<List<CategoryDTO>>
                .Success(categories, "Lấy danh sách Danh mục thành công.");
        }

        public async Task<Result<CategoryDTO>> 
            CreateAsync(CreateCategoryDTO request, CancellationToken cancellationToken)
        {
            var category = new Category(request.Name, request.Description);

            if (await _context.Categories
                .AnyAsync(c => c.Name == request.Name, cancellationToken))
            {
                return Result<CategoryDTO>
                    .Fail("Tên Danh mục đã tồn tại.", HttpStatusCode.Conflict);
            }

            await _context.Categories
                .AddAsync(category, cancellationToken);

            await _context
                .SaveChangesAsync(cancellationToken);

            var dto = new CategoryDTO(category);

            return Result<CategoryDTO>
                .Success(dto, "Thêm Danh mục thành công.", HttpStatusCode.Created);
            
        }

        public async Task<Result<CategoryDTO>> 
            UpdateAsync(Guid id, UpdateCategoryDTO request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if(category == null)
            {
                return Result<CategoryDTO>
                    .Fail("Danh mục không tồn tại.", HttpStatusCode.NotFound);
            }

            category.Update(request.Name, request.Description);

            await _context.SaveChangesAsync(cancellationToken);

            var dto = new CategoryDTO(category);

            return Result<CategoryDTO>
                .Success(dto, "Cập nhật Danh mục thành công.", HttpStatusCode.OK);
        }

        public async Task<Result> 
            DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                return Result
                    .Fail("Danh mục không tồn tại.", HttpStatusCode.NotFound);
            }

            if (category.IsDeleted)
            {
                return Result
                    .Fail("Danh mục đã bị xóa.", HttpStatusCode.Conflict);
            }

            category.SoftDelete();

            await _context.SaveChangesAsync(cancellationToken);

            return Result
                .Success("Xóa Danh mục thành công.", HttpStatusCode.OK);
        }

        public async Task<Result> 
            RestoreAsync(Guid id, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (category == null)
            {
                return Result
                    .Fail("Danh mục không tồn tại.", HttpStatusCode.NotFound);
            }

            if (!category.IsDeleted)
            {
                return Result
                    .Fail("Danh mục chưa bị xóa.", HttpStatusCode.Conflict);
            }

            category.Restore();

            await _context.SaveChangesAsync(cancellationToken);

            return Result
                .Success("Khôi phục Danh mục thành công.", HttpStatusCode.OK);
        }
    }
}
