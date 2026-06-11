namespace Restaurant.Application.Common.Models
{
    public class BaseDTO
    {
        public Guid PublicId {  get; set; }
    }

    public class AuditableDTO : BaseDTO
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SoftDeleteDTO : AuditableDTO
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
