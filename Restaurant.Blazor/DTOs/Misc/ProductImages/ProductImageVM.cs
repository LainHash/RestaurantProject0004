using Restaurant.Blazor.Common.Models;

namespace Restaurant.Blazor.DTOs.Misc.ProductImages
{
    public class ProductImageVM : AuditableVM
    {
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsPrimary { get; set; }
    }
}
