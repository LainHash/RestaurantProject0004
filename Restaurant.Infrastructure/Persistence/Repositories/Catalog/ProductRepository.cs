using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Misc;
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


        public async Task<Result<List<ProductDTO>>>
            GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .Include(p => p.ProductImages)
                .Select(pro => new ProductDTO(pro))
                .ToListAsync(cancellationToken);

            return Result<List<ProductDTO>>
                .Success(products, "Lấy danh sách Sản phẩm thành công.", HttpStatusCode.OK);
        }

        public async Task<Result<ProductDTO>>
            GetOneByPublicIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .Include(p => p.ProductImages)
                .Where(p => p.PublicId == id)
                .Select(pro => new ProductDTO(pro))
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                return Result<ProductDTO>
                    .Fail("Sản phẩm không tồn tại.", HttpStatusCode.NotFound);
            }

            return Result<ProductDTO>
                .Success(product, "Lấy Sản phẩm thành công.", HttpStatusCode.OK);
        }

        public async Task<Result<ProductDTO>>
            CreateAsync(CreateProductDTO request, CancellationToken cancellationToken)
        {
            if (await _context.Products
                .AnyAsync(p => p.Name == request.Name, cancellationToken))
            {
                return Result<ProductDTO>
                    .Fail("Tên Sản phẩm đã tồn tại.", HttpStatusCode.Conflict);
            }

            var categoryId = await GetCategoryId(request.CategoryName, cancellationToken);
            if (categoryId is not int categoryIdValue)
            {
                return Result<ProductDTO>
                    .Fail("Tên Danh mục không tồn tại.", HttpStatusCode.NotFound);
            }

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var product = new Product(request.Name, request.Description, request.IsMadeToOrder, categoryIdValue);

                _context.Products.Add(product);

                await _context.SaveChangesAsync(cancellationToken);

                var productStock = new ProductStock(request.Price, request.Unit, request.Quantity, product.Id);

                _context.ProductStocks.Add(productStock);

                if (request.Images.Count > 0)
                {
                    var productImages = request.Images
                        .Select((url, index) => new ProductImage(url, isPrimary: index == 0, product.Id))
                        .ToList();

                    _context.ProductImages.AddRange(productImages);
                }

                await _context.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);

                var created = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductStock)
                    .Include(p => p.ProductImages)
                    .Where(p => p.Id == product.Id)
                    .FirstAsync(cancellationToken);

                return Result<ProductDTO>
                    .Success(new ProductDTO(created), "Thêm sản phẩm thành công.", HttpStatusCode.Created);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Result<ProductDTO>> 
            UpdateAsync(Guid id, UpdateProductDTO request, CancellationToken cancellationToken)
        {
            var categoryId = await GetCategoryId(request.CategoryName, cancellationToken);
            if (categoryId is not int categoryIdValue)
            {
                return Result<ProductDTO>
                    .Fail("Tên Danh mục không tồn tại.", HttpStatusCode.NotFound);
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .Include(p => p.ProductImages)
                .Where(p => p.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);
            if(product == null)
            {
                return Result<ProductDTO>
                    .Fail("Sản phẩm không tồn tại.", HttpStatusCode.NotFound);
            }

            product.Update(request.Name, request.Description, request.IsAvailable, request.IsMadeToOrder, categoryIdValue);

            product.ProductStock.Update(request.Price, request.Unit, request.Quantity);

            await _context.SaveChangesAsync();

            var updated = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductStock)
                    .Include(p => p.ProductImages)
                    .Where(p => p.Id == product.Id)
                    .FirstAsync(cancellationToken);

            return Result<ProductDTO>
                .Success(new ProductDTO(updated), "Cập nhật Sản phẩm thành công.", HttpStatusCode.OK);
        }

        public async Task<Result> 
            DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(p => p.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);
            if( product == null)
            {
                return Result
                    .Fail("Sản phẩm không tồn tại.", HttpStatusCode.NotFound);
            }
            if (product.IsDeleted)
            {
                return Result
                    .Fail("Sản phẩm đã bị xóa trước đó.", HttpStatusCode.Conflict);
            }

            product.Delete();

            await _context.SaveChangesAsync();

            return Result
                .Success("Xóa Sản phẩm thành công.", HttpStatusCode.OK);
            
        }

        public async Task<Result> 
            RestoreAsync(Guid id, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Where(p => p.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);
            if (product == null)
            {
                return Result
                    .Fail("Sản phẩm không tồn tại.", HttpStatusCode.NotFound);
            }
            if (!product.IsDeleted)
            {
                return Result
                    .Fail("Sản phẩm chưa bị xóa.", HttpStatusCode.Conflict);
            }

            product.Restore();

            await _context.SaveChangesAsync();

            return Result
                .Success("Khôi phục Sản phẩm thành công.", HttpStatusCode.OK);
        }

        public async Task<Result<ProductDTO>>
            ChangeImagesAsync(Guid id, ChangeImagesProductDTO request, CancellationToken cancellationToken)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductStock)
                .Include(p => p.ProductImages)
                .Where(p => p.PublicId == id)
                .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
            {
                return Result<ProductDTO>
                    .Fail("Sản phẩm không tồn tại.", HttpStatusCode.NotFound);
            }

            if (request.NewPrimaryImageUrl != null)
            {
                var existingUrls = product.ProductImages.Select(i => i.ImageUrl).ToHashSet();
                var addingUrls = request.ImagesToAdd.ToHashSet();
                bool isRemoving = request.ImagesToRemove.Contains(request.NewPrimaryImageUrl);

                bool willExist = (existingUrls.Contains(request.NewPrimaryImageUrl) || addingUrls.Contains(request.NewPrimaryImageUrl))
                                 && !isRemoving;

                if (!willExist)
                {
                    return Result<ProductDTO>
                        .Fail("URL ảnh primary không tồn tại trong danh sách ảnh của sản phẩm sau khi cập nhật.",
                              HttpStatusCode.BadRequest);
                }
            }

            using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
            try
            {
                var currentImages = product.ProductImages.ToList();

                if (request.ImagesToRemove.Count > 0)
                {
                    var removeSet = request.ImagesToRemove.ToHashSet();
                    var imagesToDelete = currentImages
                        .Where(i => removeSet.Contains(i.ImageUrl))
                        .ToList();

                    if (imagesToDelete.Count > 0)
                    {
                        _context.ProductImages.RemoveRange(imagesToDelete);
                        currentImages = currentImages.Except(imagesToDelete).ToList();
                    }
                }

                if (request.ImagesToAdd.Count > 0)
                {
                    var existingUrls = currentImages.Select(i => i.ImageUrl).ToHashSet();
                    var newImages = request.ImagesToAdd
                        .Where(url => !existingUrls.Contains(url))
                        .Select(url => new ProductImage(url, isPrimary: false, product.Id))
                        .ToList();

                    if (newImages.Count > 0)
                    {
                        _context.ProductImages.AddRange(newImages);
                        currentImages.AddRange(newImages);
                    }
                }

                if (request.NewPrimaryImageUrl != null)
                {
                    foreach (var img in currentImages.Where(i => i.IsPrimary))
                    {
                        img.IsPrimary = false;
                    }
                    var newPrimary = currentImages
                        .FirstOrDefault(i => i.ImageUrl == request.NewPrimaryImageUrl);
                    if (newPrimary != null)
                    {
                        newPrimary.IsPrimary = true;
                    }
                }
                else
                {
                    bool hasPrimary = currentImages.Any(i => i.IsPrimary);
                    if (!hasPrimary && currentImages.Count > 0)
                    {
                        currentImages[0].IsPrimary = true;
                    }
                }

                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                var updated = await _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.ProductStock)
                    .Include(p => p.ProductImages)
                    .Where(p => p.Id == product.Id)
                    .FirstAsync(cancellationToken);

                return Result<ProductDTO>
                    .Success(new ProductDTO(updated), "Cập nhật ảnh sản phẩm thành công.", HttpStatusCode.OK);
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        private async Task<int?> GetCategoryId(string name, CancellationToken cancellationToken)
        {
            var category = await _context.Categories
                .Where(c => c.Name == name)
                .FirstOrDefaultAsync(cancellationToken);

            return category?.Id;
        }

        
    }
}
