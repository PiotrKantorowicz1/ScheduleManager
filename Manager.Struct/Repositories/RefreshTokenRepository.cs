using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.EF;

namespace Manager.Struct.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository, ISqlRepository
    {
        public RefreshTokenRepository(ManagerDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await GetSingleAsync(x => x.Token == token);

        public async Task CreateTokenAsync(RefreshToken token)
            => await AddAsync(token);

        public async Task UpdateTokenAsync(RefreshToken token)
            => await UpdateAsync(token);
    }
}
