using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Manager.Struct.DTO;
using Manager.Struct.Extensions;
using Manager.Struct.Settings;
using Microsoft.IdentityModel.Tokens;
using NLog;

namespace Manager.Struct.Services
{
    public class JwtHandler : IJwtHandler
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly JwtOptions _options;
        private readonly SecurityKey _issuerSigningKey;
        private readonly SigningCredentials _signingCredentials;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtHandler(JwtOptions options)
        {
            _options = options;
            _issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));
            _signingCredentials = new SigningCredentials(_issuerSigningKey, SecurityAlgorithms.HmacSha256);
            _tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = _issuerSigningKey,
                ValidIssuer = _options.Issuer,
                ValidAudience = _options.ValidAudience,
                ValidateAudience = _options.ValidateAudience,
                ValidateLifetime = _options.ValidateLifetime
            };
        }

        public JsonWebToken CreateToken(Guid serialNumber, string role)
        {
            var now = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, serialNumber.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, serialNumber.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToTimestamp().ToString()),
                new Claim(ClaimTypes.Role, role)
            };
            var expires = now.AddMinutes(_options.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JsonWebToken
            {
                AccessToken = token,
                Expires = expires.ToTimestamp()
            };
        }

        public JsonWebTokenPayload GetTokenPayload(string accessToken)
        {
            try
            {
                _jwtSecurityTokenHandler.ValidateToken(accessToken, _tokenValidationParameters,
                    out SecurityToken validatedSecurityToken);
                var jwt = validatedSecurityToken as JwtSecurityToken;

                return new JsonWebTokenPayload
                {
                    Subject = jwt.Subject,
                    Role = jwt.Claims.Single(x => x.Type == ClaimTypes.Role).Value,
                    Expires = jwt.ValidTo.ToTimestamp()
                };
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Could not get JWT payload. " + ex.Message);

                return null;
            }
        }
    }
}
