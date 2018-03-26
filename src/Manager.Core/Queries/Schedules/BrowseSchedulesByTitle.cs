using Manager.Core.Types;

namespace Manager.Core.Queries.Schedules
{
    public class BrowseSchedulesByTitle : PagedQueryBase
    {
        public string Titile { get; set; }
    }
}
