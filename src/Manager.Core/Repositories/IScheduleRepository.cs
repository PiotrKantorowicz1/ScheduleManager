using System.Threading.Tasks;
using Manager.Core.Models;

namespace Manager.Core.Repositories
{
    public interface IScheduleRepository : IRepositoryBase<Schedule>
    {         
        Task<Schedule> GetAsync(int id);
        Task<Schedule> GetByAsync(int id);
    }
}