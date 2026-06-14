using Restaurant.Domain.Common.Models;

namespace Restaurant.Domain.Entities.Catalog
{
    public class Category : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;

        public virtual IEnumerable<Product> Products { get; set; } = new List<Product>();

        public Category() { }
        public Category(string name, string? description = "")
        {
            Name = name;
            Description = description;
        }

        public void Update(string name, string? description = "")
        {
            Name = name;
            Description = description;
        }

        public void SoftDelete() 
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedAt = null;
        }
    }
}
