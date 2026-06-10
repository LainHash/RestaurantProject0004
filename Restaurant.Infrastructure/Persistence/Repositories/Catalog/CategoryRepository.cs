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
                .Select(c => new CategoryDTO
                {
                    Name = c.Name,
                    Description = c.Description,
                })
                .ToListAsync(cancellationToken);
            return Result<List<CategoryDTO>>
                .Success(categories, "Lấy danh sách Danh mục thành công.");
        }

        public async Task<Result<CategoryDTO>> 
            CreateAsync(CreateCategoryDTO request, CancellationToken cancellationToken)
        {
            try
            {
                var category = new Category()
                {
                    Name = request.Name,
                    Description = request.Description ?? string.Empty,
                };

                if (await _context.Categories.AnyAsync(c => c.Name == request.Name))
                {
                    return Result<CategoryDTO>
                        .Fail("Tên Danh mục đã tồn tại.", HttpStatusCode.Conflict);
                }

                _context.Categories.Add(category);
                await _context.SaveChangesAsync();

                var dto = new CategoryDTO()
                {
                    Name = category.Name,
                    Description = category.Description,
                };
                return Result<CategoryDTO>
                    .Success(dto, "Thêm Danh mục thành công.", HttpStatusCode.Created);
            }
            catch (Exception ex) {
                return Result<CategoryDTO>
                    .Fail($"Thêm Danh mục thất bại\n {ex}", HttpStatusCode.InternalServerError);
            }
            
        }

        public Task<Result<CategoryDTO>> 
            UpdateAsync(Guid id, UpdatedCategoryDTO request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<Result<CategoryDTO>> 
            DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
