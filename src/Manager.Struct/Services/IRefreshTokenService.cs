using System.Threading.Tasks;
using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface IRefreshTokenService : IService
    {
        Task CreateAsync(int userId);
        Task<JsonWebToken> CreateAccessTokenAsync(string refreshToken);
        Task RevokeAsync(string refreshToken, int userId);
    }
}
