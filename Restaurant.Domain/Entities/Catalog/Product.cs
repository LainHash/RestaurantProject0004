using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Inventory;
using Restaurant.Domain.Entities.Misc;

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
        public virtual IEnumerable<ProductImage> ProductImages { get; set; } = new List<ProductImage>();


        public Product()
        {

        }

        public Product(string name, string? description, bool isMadeToOrder, int categoryId)
        {
            Name = name;
            Description = description ?? string.Empty;
            IsAvailable = true;
            IsMadeToOrder = isMadeToOrder;
            CategoryId = categoryId;
        }

        public void Update(string name, string? description, bool isAvilable, bool isMadeToOrder, int categoryId)
        {
            Name = name;
            Description = description ?? string.Empty;
            IsAvailable = IsAvailable;
            IsMadeToOrder = isMadeToOrder;
            CategoryId = categoryId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            UpdatedAt = DateTime.UtcNow;
            DeletedAt = null;
        }
    }
}
