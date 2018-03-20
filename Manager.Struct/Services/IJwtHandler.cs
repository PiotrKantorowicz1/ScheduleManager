using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(int userId, string role);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
