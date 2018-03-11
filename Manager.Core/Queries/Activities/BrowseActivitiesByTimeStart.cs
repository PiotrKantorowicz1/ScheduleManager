using System;
using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByTimeStart : PagedQueryBase
    {
        public DateTime TimeStart { get; set; }
    }
}
