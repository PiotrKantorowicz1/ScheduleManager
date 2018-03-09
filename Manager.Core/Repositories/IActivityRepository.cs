using System.Threading.Tasks;
using Manager.Core.Models;

namespace Manager.Core.Repositories
{
    public interface IActivityRepository : IRepositoryBase<Activity>
    {        
        Task<Activity> GetAsync(int id);      
    }
}