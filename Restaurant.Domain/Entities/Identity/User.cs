using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities.Identity
{
    public class User : SoftDeleteEntity
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Status { get; set; } = null!;

        public int PIId { get; set; }
        public int RolerId { get; set; }

        public virtual PersonalInformation PersonalInformation { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;

        public virtual Customer? Customer { get; set; }
    }
}
