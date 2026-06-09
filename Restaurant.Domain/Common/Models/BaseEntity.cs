namespace Restaurant.Domain.Common.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
    }

    public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SoftDeleteEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
