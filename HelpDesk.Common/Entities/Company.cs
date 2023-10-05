using System;
using System.Collections.Generic;

namespace HelpDesk.Common.Entities
{
    public partial class Company
    {
        public Company()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public Guid CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Mobile { get; set; }
        public bool IsEnabled { get; set; } = false;

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
