using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;
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
    }
}
