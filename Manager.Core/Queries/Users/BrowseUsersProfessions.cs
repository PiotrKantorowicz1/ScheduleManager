using Manager.Core.Types;

namespace Manager.Core.Queries.Users
{
    public class BrowseUsersProfessions : PagedQueryBase
    {
        public string Profession { get; set; }
    }
}
