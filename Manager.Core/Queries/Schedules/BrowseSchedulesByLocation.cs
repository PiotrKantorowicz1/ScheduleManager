using Manager.Core.Types;

namespace Manager.Core.Queries.Schedules
{
    public class BrowseSchedulesByLocation : PagedQueryBase
    {
        public string Location { get; set; }
    }
}
