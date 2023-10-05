using System;
using System.Collections.Generic;

namespace HelpDesk.Common.Entities
{
    public partial class Contact
    {
        public Contact()
        {
            Customers = new HashSet<Customer>();
        }

        public Guid ContactId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool? IsEnabled { get; set; }

        public string FullName => $"{FirstName} {LastName}";
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
