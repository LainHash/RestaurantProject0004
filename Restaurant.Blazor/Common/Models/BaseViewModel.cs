namespace Restaurant.Blazor.Common.Models
{
    public class BaseViewModel
    {
        public Guid PublicId { get; set; }
    }

    public class AuditableVM : BaseViewModel
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class SoftDeleteVM : AuditableVM
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
