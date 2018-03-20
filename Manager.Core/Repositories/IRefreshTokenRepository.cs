using System.Threading.Tasks;
using Manager.Core.Models;

namespace Manager.Core.Repositories
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
        Task<RefreshToken> GetAsync(string token);
        Task CreateTokenAsync(RefreshToken token);
        Task UpdateTokenAsync(RefreshToken token);
    }
}
