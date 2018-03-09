using Manager.Core.Models;
using Manager.Struct.EF;
using Manager.Core.Repositories;
using System.Threading.Tasks;

namespace Manager.Struct.Repositories
{
    public class ScheduleRepository : RepositoryBase<Schedule>, IScheduleRepository, ISqlRepository
    {
        public ScheduleRepository(ManagerDbContext context) : base(context)
        {
        }

        public async Task<Schedule> GetAsync(int id)
            => await GetSingleAsync(s => s.Id == id, s => s.Creator, s => s.Attendees);

        public async Task<Schedule> GetByAsync(int id)
            => await GetSingleAsync(s => s.Id == id);
    }
}