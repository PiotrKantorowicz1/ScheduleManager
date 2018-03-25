using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.EF;

namespace Manager.Struct.Repositories
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository, ISqlRepository
    {
        public RefreshTokenRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<RefreshToken> GetAsync(string token)
            => await GetSingleAsync(x => x.Token == token);

        public async Task CreateTokenAsync(RefreshToken token)
            => await AddAsync(token);

        public void UpdateTokenAsync(RefreshToken token)
            => Update(token);
    }
}
