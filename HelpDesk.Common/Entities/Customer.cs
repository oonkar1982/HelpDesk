using HelpDesk.Common.Models;
using System;
using System.Collections.Generic;

namespace HelpDesk.Common.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Incidents = new HashSet<Incident>();
        }

        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? AccountNumber { get; set; }
        public Guid? PrimaryContactId { get; set; }
        public string? Description { get; set; }
        public string? WebSiteUrl { get; set; }
        public string? EmailAddress1 { get; set; }
        public string? EmailAddress2 { get; set; }
        public string? Telephone1 { get; set; }
        public string? Telephone2 { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Guid? OwnerId { get; set; }

        public virtual UserDetail? Owner { get; set; }
        public virtual Contact? PrimaryContact { get; set; }
        public virtual ICollection<Incident> Incidents { get; set; }
        
      
    }
}
