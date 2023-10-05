using HelpDesk.Common.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HelpDesk.Common.Models
{
    public class IncidentModel
    {
        public Guid IncidentId { get; set; }

        [DisplayName]
        public string? CaseOrigin { get; set; }
        public string? CaseType { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? TicketNumber { get; set; }
        public string? Priority { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? Status { get; set; }
        public string? Title { get; set; }

        public string? Customer { get; set; }
        
        
    }
    public class IncidentCreateModel
    {
        public Guid IncidentId { get; set; }

        [Display(Name = "Case Origin")]
        public int? CaseOriginCode { get; set; }

        [Display(Name = "Case Type")]
        public int? CaseTypeCode { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        [Display(Name = "Case No.")]
        public string? TicketNumber { get; set; }

        [Display(Name = "Content")]
        public string? description { get; set; }

        [Display(Name = "Priority")]
        public int? PriorityCode { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public int StateCode { get; set; }
        public int? StatusCode { get; set; }

        [Display(Name = "Customer")]
        public Guid? CustomerId { get; set; }
        public Guid? OwnerId { get; set; }
        
        public virtual List<SelectListItem>? Customers { get; set; }
        public List<dropdownelement>? droplists { get; set; }
    }
    public class EditIncidentModel
    {
        public IncidentCreateModel? incident { get; set; }
        public virtual List<SelectListItem>? Owners { get; set; }
        public virtual List<SelectListItem>? Customers { get; set; }
        public List<dropdownelement>? droplists { get; set; }

    }
    public class dropdownelement
    {
        public int? AttributeValue { get; set; } = null;
        public string? DisplayValue { get; set; } = null;
        public string? AttributeName { get; set; } = null;

    }

  }
