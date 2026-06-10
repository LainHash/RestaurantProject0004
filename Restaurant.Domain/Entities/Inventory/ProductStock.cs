using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Domain.Entities.Inventory
{
    public class ProductStock : BaseEntity
    {
        public decimal Price { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Quantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
