using Manager.Core.Types;

namespace Manager.Core.Queries.Schedules
{
    public class BrowseSchedulesByType : PagedQueryBase
    {
        public int Type { get; set; }
    }
}
