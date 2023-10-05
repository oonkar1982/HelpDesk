using HelpDesk.Common.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Common.Models
{

    public class CustomerModel
    {
        public Guid Id { get; set; }
        public EditCustomerModel editCustomerModel { get; set; } = new EditCustomerModel();        
        public List<IncidentModel>? Incidentlist { get; set; }= new List<IncidentModel>();
    }
    public class CreateCustomerModel
    {
        public Guid CustomerId { get; set; }

        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "Please enter Organization Name"), MaxLength(50)]
        public string? Name { get; set; }

        [Display(Name = "AccountNumber")]
        [Required(ErrorMessage = "Please enter AccountNumber"), MaxLength(10)]
        public string? AccountNumber { get; set; }
        public Guid? PrimaryContactId { get; set; }
        public virtual Contact? PrimaryContact { get; set; }
        [Display(Name = "Description")]
        public string? Description { get; set; }
        [Display(Name = "WebSite")]
        public string? WebSiteUrl { get; set; }

        [Display(Name = "Email 1")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? EmailAddress1 { get; set; }
        [Display(Name = "Email 2")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? EmailAddress2 { get; set; }
        [Display(Name = "Telephone 1")]
        public string? Telephone1 { get; set; }
        [Display(Name = "Telephone 2")]
        public string? Telephone2 { get; set; }
        
    }

    public class EditCustomerModel
    {
        public Guid CustomerId { get; set; }
        public string? Name { get; set; }
        public string? AccountNumber { get; set; }
        public Guid? PrimaryContactId { get; set; }
        public virtual Contact? Contacts { get; set; }
        public string? Description { get; set; }
        public string? WebSiteUrl { get; set; }
        public string? EmailAddress1 { get; set; }
        public string? EmailAddress2 { get; set; }
        public string? Telephone1 { get; set; }
        public string? Telephone2 { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public Guid? OwnerId { get; set; }       
        public virtual  UserModel? Owners { get; set; } 
        

    }

    public class CustomerView
    {
        public Guid CustomerId { get; set; }

        [Display(Name = "Organization Name")]
        [Required(ErrorMessage = "Please enter Organization Name"), MaxLength(50)]
        public string? Name { get; set; }

        [Display(Name = "AccountNumber")]
        [Required(ErrorMessage = "Please enter AccountNumber"), MaxLength(10)]
        public string? AccountNumber { get; set; }

        [Display(Name = "Contact Person")]
        public string? PrimaryContact { get; set; }
        [Display(Name = "Telephone")]
        public string? Telephone { get; set; }
        [Display(Name = "E-mail")]
        public string? Email { get; set; }
        public string? Status { get; set; }
    }

}
