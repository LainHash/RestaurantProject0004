using Restaurant.Domain.Common.Models;

namespace Restaurant.Domain.Entities.Identity
{
    public class PersonalInformation : SoftDeleteEntity
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly DOB { get; set; }
        public bool Gender { get; set; }
        public string Country { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string CitizenCardId { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
