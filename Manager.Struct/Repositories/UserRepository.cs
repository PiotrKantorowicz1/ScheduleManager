using Manager.Core.Models;
using Manager.Struct.EF;
using Manager.Core.Repositories;
using System.Threading.Tasks;

namespace Manager.Struct.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository, ISqlRepository
    {
        public UserRepository(ManagerDbContext context) : base(context)
        {
        }

        public async Task<User> GetAsync(int id)
            => await GetSingleAsync(u => u.Id == id);

        public async Task<User> GetAsync(string name)
            => await GetSingleAsync(u => u.Name == name);
    }
}