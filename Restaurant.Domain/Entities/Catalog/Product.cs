using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Product : SoftDeleteEntity
    {
        public Guid PublicId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsMadeToOrder { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ProductStock ProductStock { get; set; } = null!;
    }
}
