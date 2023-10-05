
using HelpDesk.Common.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HelpDesk.Common.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? Email { get; set; }

        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }
       public bool? IsEnabled { get; set; }
        //public string? Password { get; set; }
        public string? CompanyName { get; set; }

        public string? FullName => $"{FirstName} {LastName}";
        public UserModel( UserDetail user)
        {
            Id = user.Id;FirstName = user.FirstName;LastName=user.LastName;Email = user.Email; Mobile = user.Mobile; IsEnabled = user.IsEnabled;
            CompanyName = user.Company?.Name;
        }

        public UserModel()
        {
        }
    }
    

    public class LoginModel
    {
        public string? Email { get; set; } = null;
        public string? Password { get; set; } = null;

    }
    public class UserEditModel
    {

        public Guid Id { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Your Email is not valid.")]
        public string? Email { get; set; }

        [Display(Name = "Mobile")]
        public string? Mobile { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public Guid? CompanyId { get; set; }

        public List<SelectListItem>? Companylist { get; set;}
    }
}
