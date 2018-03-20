using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Accounts
{
    public class RevokeHandler : ICommandHandler<Revoke>
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RevokeHandler(IRefreshTokenService refreshTokenService)
        {
            _refreshTokenService = refreshTokenService;
        }
        public async Task HandleAsync(Revoke command)
        {
            await _refreshTokenService.RevokeAsync(command.RefreshToken, command.UserId);
        }
    }
}