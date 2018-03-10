using Manager.Core.Types;

namespace Manager.Core.Queries.Users
{
    public class BrowseUsesrRoles : PagedQueryBase
    {
        public string Role { get; set; }
    }
}
