using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Application.Features.Catalog.Products.DTOs
{
    public class ProductDTO : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsMadeToOrder { get; set; }

        public decimal Price { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Quantity { get; set; }

        public string CategoryName { get; set; } = null!;

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
        }
    }
}
