using Restaurant.Blazor.Common.Models;
using Restaurant.Blazor.DTOs.Misc.ProductImages;

namespace Restaurant.Blazor.DTOs.Catalog.Products
{
    public class ProductVM : SoftDeleteVM
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

        public IEnumerable<ProductImageVM> Images = new List<ProductImageVM>();
    }
}
