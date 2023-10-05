using System;
using System.Collections.Generic;

namespace HelpDesk.Common.Entities
{ 
    public partial class Incident
    {
        public Guid IncidentId { get; set; }
        public int? CaseOriginCode { get; set; }
        public int? CaseTypeCode { get; set; }
        public string? Title { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? TicketNumber { get; set; }
        public string? description { get; set; }
        public int? PriorityCode { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public int StateCode { get; set; }
        public int? StatusCode { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? OwnerId { get; set; }
        public int OwnerIdType { get; set; }
        public bool? ActivitiesComplete { get; set; }
        public Guid? ExistingCase { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual UserDetail? Owner { get; set; }

        
    }
}
