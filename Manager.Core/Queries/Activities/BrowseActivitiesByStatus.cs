using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByStatus : PagedQueryBase
    {
        public int Status { get; set; }
    }
}
