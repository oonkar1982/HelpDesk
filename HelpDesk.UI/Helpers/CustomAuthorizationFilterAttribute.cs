using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HelpDesk.UI.Helpers
{
    //public class CustomAuthorizationFilterAttribute : AuthorizeAttribute, IAuthorizationFilter
    //{
    //    public string[] Permissions { get; set; }
    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        //Validate if any permissions are passed when using attribute at controller or action level
    //        if (string.IsNullOrEmpty(Permissions.Split(",")))
    //        {
    //            //Validation cannot take place without any permissions so returning unauthorized
    //            context.Result = new UnauthorizedResult();
    //            return;
    //        }            
    //        var userName = context.HttpContext.User.Identity.Name;
    //        var assignedPermissionsForUser = MockData.UserPermissions.Where(x => x.Key == userName).Select(x => x.Value).ToList();


    //        var requiredPermissions = Permissions.Split(","); //Multiple permissiosn can be received from controller, delimiter "," is used to get individual values
    //        foreach (var x in requiredPermissions)
    //        {
    //            if (assignedPermissionsForUser.Contains(x))
    //                return; //User Authorized. Wihtout setting any result value and just returning is sufficent for authorizing user
    //        }

    //        context.Result = new UnauthorizedResult();
    //        return;
    //    }
    //}
}
