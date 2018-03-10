using Manager.Core.Models;
using Manager.Struct.EF;
using Manager.Core.Repositories;
using System.Threading.Tasks;

namespace Manager.Struct.Repositories
{
    public class ActivityRepository : RepositoryBase<Activity>, IActivityRepository, ISqlRepository
    {
        public ActivityRepository(ManagerDbContext context) : base(context)
        {
        }

        public async Task<Activity> GetAsync(int id)
            => await GetSingleAsync(a => a.Id == id);
    }
}