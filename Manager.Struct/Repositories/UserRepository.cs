using Manager.Core.Models;
using Manager.Struct.EF;
using Manager.Core.Repositories;
using System.Threading.Tasks;

namespace Manager.Struct.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository, ISqlRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<User> GetAsync(int id)
            => await GetSingleAsync(u => u.Id == id);

        public async Task<User> GetAsync(string name)
            => await GetSingleAsync(u => u.Name == name);

        public async Task<User> GetByEmailAsync(string email)
            => await GetSingleAsync(u => u.Email == email);
    }
}