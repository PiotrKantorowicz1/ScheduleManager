using Manager.Core.Types;

namespace Manager.Core.Queries.Users
{
    public class BrowseUsersByRole : PagedQueryBase
    {
        public string Role { get; set; }
    }
}
