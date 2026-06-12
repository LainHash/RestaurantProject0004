using Restaurant.Application.Common.Models;
using Restaurant.Domain.Entities.Catalog;
using Restaurant.Domain.Entities.Misc;

namespace Restaurant.Application.Features.Catalog.Products.DTOs
{
    public class ProductDTO : SoftDeleteDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsMadeToOrder { get; set; }

        public decimal Price { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Quantity { get; set; }

        public string CategoryName { get; set; } = null!;

        public string PrimaryImage { get; set; } = null!;

        public IEnumerable<ImageDTO> Images { get; set; } = new List<ImageDTO>();

        public ProductDTO(Product product)
        {
            PublicId = product.PublicId;
            Name = product.Name;
            Description = product.Description ?? string.Empty;
            IsAvailable = product.IsAvailable;
            IsMadeToOrder = product.IsMadeToOrder;
            Price = product.ProductStock.Price;
            Unit = product.ProductStock.Unit;
            Quantity = product.ProductStock.Quantity;
            CategoryName = product.Category.Name;
            CreatedAt = product.CreatedAt;
            UpdatedAt = product.UpdatedAt;
            IsDeleted = product.IsDeleted;
            DeletedAt = product.DeletedAt;

            var images = product.ProductImages?.ToList() ?? new List<ProductImage>();
            PrimaryImage = images.FirstOrDefault(i => i.IsPrimary)?.ImageUrl
                        ?? images.FirstOrDefault()?.ImageUrl
                        ?? string.Empty;
            Images = images.Select(i => new ImageDTO(i)).ToList();
        }
    }

    public class ImageDTO : AuditableDTO
    {
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }

        public ImageDTO(ProductImage images)
        {
            ImageUrl = images.ImageUrl;
            IsPrimary = images.IsPrimary;
        }
    }
}
