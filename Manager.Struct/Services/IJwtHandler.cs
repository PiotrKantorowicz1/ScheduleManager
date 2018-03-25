using System;
using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface IJwtHandler
    {
        JsonWebToken CreateToken(Guid serialNumber, string role);
        JsonWebTokenPayload GetTokenPayload(string accessToken);
    }
}
