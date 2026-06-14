using Restaurant.Domain.Common.Models;

namespace Restaurant.Domain.Entities.Identity
{
    public class Role : SoftDeleteEntity
    {
        public string Name { get; set; } = null!;
        public int Level { get; set; }
        public string? Description { get; set; }

        public virtual IEnumerable<User> Users { get; set; } = new List<User>();
    }
}
