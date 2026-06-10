using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using System.Net;

namespace Restaurant.Infrastructure.Persistence.Repositories.Catalog
{
    public class ProductRepository : IProductRepository
    {
        private readonly RestaurantDbContext _context;
        public ProductRepository(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<ProductDTO>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .Select(p => new ProductDTO
                {
                    PublicId = p.PublicId,
                    Name = p.Name,
                    Description = p.Description ?? string.Empty,
                    IsAvailable = p.IsAvailable,
                    IsMadeToOrder = p.IsMadeToOrder,
                    Price = p.ProductStock.Price,
                    Unit = p.ProductStock.Unit,
                    Quantity = p.ProductStock.Quantity,
                    CategoryName = p.Category.Name,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync(cancellationToken);

            return Result<List<ProductDTO>>
                .Success(products, "Lấy danh sách Sản phẩm thành công.", HttpStatusCode.OK);
        }

        public async Task<Result<ProductDTO>> GetByPublicIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .Where(p => p.PublicId == id)
                .Select(p => new ProductDTO
                {
                    PublicId = p.PublicId,
                    Name = p.Name,
                    Description = p.Description ?? string.Empty,
                    IsAvailable = p.IsAvailable,
                    IsMadeToOrder = p.IsMadeToOrder,
                    Price = p.ProductStock.Price,
                    Unit = p.ProductStock.Unit,
                    Quantity = p.ProductStock.Quantity,
                    CategoryName = p.Category.Name,
                    CreatedAt = p.CreatedAt
                })
                .FirstOrDefaultAsync(cancellationToken);

            if(product == null)
            {
                return Result<ProductDTO>
                    .Fail("Sản phẩm không tồn tại.", HttpStatusCode.NotFound);
            }

            return Result<ProductDTO>
                .Success(product, "Lấy Sản phẩm thành công.", HttpStatusCode.OK);
        }
    }
}
