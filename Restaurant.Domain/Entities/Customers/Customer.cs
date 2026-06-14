using Restaurant.Domain.Common.Models;
using Restaurant.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Domain.Entities.Customers
{
    public class Customer : SoftDeleteEntity
    {
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
