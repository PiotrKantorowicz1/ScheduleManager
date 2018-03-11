using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByType : PagedQueryBase
    {
        public int Type { get; set; }
    }
}
