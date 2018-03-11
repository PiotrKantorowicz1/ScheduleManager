using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByPriority : PagedQueryBase
    {
        public int Priority { get; set; }
    }
}
