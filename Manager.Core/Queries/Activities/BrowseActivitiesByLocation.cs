using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByLocation : PagedQueryBase
    {
        public string Location { get; set; }
    }
}
