using Restaurant.Domain.Common.Models;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Category : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;

        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();
    }
}
