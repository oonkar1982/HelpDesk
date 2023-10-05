using System;
using System.Collections.Generic;

namespace HelpDesk.Common.Entities
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            Customers = new HashSet<Customer>();
            Incidents = new HashSet<Incident>();
        }

        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool? IsEnabled { get; set; }
        public string? Password { get; set; }
        public Guid? CompanyId { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public virtual Company? Company { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
    }
}
