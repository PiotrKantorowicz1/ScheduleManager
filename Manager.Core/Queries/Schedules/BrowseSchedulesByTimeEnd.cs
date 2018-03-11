using System;
using Manager.Core.Types;

namespace Manager.Core.Queries.Schedules
{
    public class BrowseSchedulesByTimeEnd : PagedQueryBase
    {
        public DateTime TimeEnd { get; set; }
    }
}
