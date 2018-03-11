using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByTitle : PagedQueryBase
    {
        public string Title { get; set; }
    }
}
