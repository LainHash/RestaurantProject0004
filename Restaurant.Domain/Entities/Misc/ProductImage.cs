using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Misc
{
    public class ProductImage : AuditableEntity
    {
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;

        public ProductImage() { }

        public ProductImage(string imageUrl, bool isPrimary, int productId)
        {
            ImageUrl = imageUrl;
            IsPrimary = isPrimary;
            ProductId = productId;
        }
    }
}
