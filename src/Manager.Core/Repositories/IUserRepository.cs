using System.Threading.Tasks;
using Manager.Core.Models;

namespace Manager.Core.Repositories
{
    public interface IUserRepository : IRepositoryBase<User>
    {      
        Task<User> GetAsync(int id);
        Task<User> GetAsync(string name);
        Task<User> GetByEmailAsync(string email);
    }
}