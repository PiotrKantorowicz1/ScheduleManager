using System.Text;
using Manager.Struct.Services;
using Manager.Struct.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Manager.Struct.Extensions
{
    public static class JwtExtensions
    {
        public static void AddJwt(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            var section = configuration.GetSection("jwt");
            var options = new JwtOptions();
            section.Bind(options);
            services.AddSingleton(options);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)),
                        ValidIssuer = options.Issuer,
                        ValidAudience = options.ValidAudience,
                        ValidateAudience = options.ValidateAudience,
                        ValidateLifetime = options.ValidateLifetime
                    };
                });
        }
    }
}
