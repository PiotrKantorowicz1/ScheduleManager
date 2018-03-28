using Manager.Core.Types;

namespace Manager.Core.Queries.Schedules
{
    public class BrowseSchedulesByCreator : PagedQueryBase
    {
        public int CreatorId { get; set; }
    }
}
