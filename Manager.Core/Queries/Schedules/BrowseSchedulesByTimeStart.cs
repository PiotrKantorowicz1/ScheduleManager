using System;
using Manager.Core.Types;

namespace Manager.Core.Queries.Schedules
{
    public class BrowseSchedulesByTimeStart : PagedQueryBase
    {
        public DateTime TimeStart { get; set; }
    }
}
