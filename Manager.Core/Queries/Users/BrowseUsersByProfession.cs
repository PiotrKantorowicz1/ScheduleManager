using Manager.Core.Types;

namespace Manager.Core.Queries.Users
{
    public class BrowseUsersByProfession : PagedQueryBase
    {
        public string Profession { get; set; }
    }
}
