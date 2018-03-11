using System;
using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByTimeEnd : PagedQueryBase
    {
        public DateTime TimeEnd { get; set; }
    }
}
