using Manager.Core.Types;

namespace Manager.Core.Queries.Activities
{
    public class BrowseActivitiesByCreator : PagedQueryBase
    {
        public int CreatorId { get; set; }
    }
}
