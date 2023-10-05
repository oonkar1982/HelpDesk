namespace HelpDesk.UI.Helpers
{
    public class UserPermissions
    {        
        public ICollection<EntityPermissions> permissions { get; set; }
    }

    public class EntityPermissions {

        public bool CanDelete { get; set; } = false;
        public bool CanEdit { get; set; } = false;
        public bool CanRead { get; set; } = false;

    }


}
