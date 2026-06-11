using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Inventory;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Product : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
        public bool IsAvailable { get; set; }
        public bool IsMadeToOrder { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; } = null!;
        public virtual ProductStock ProductStock { get; set; } = null!;

        public Product(string name, string? description, bool isMadeToOrder, int categoryId)
        {
            Name = name;
            Description = description ?? string.Empty;
            IsAvailable = true;
            IsMadeToOrder = isMadeToOrder;
            CategoryId = categoryId;
        }
    }
}
