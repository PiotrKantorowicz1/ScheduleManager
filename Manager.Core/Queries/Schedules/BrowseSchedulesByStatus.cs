using Manager.Core.Types;

namespace Manager.Core.Queries.Schedules
{
    public class BrowseSchedulesByStatus : PagedQueryBase
    {
        public int Status { get; set; }
    }
}
