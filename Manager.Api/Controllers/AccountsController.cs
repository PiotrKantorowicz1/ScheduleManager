using System.Threading.Tasks;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers
{
    [Route("api/[Controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountService _accounteService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AccountsController(IAccountService accounteService,
            IRefreshTokenService refreshTokenService)
        {
            _accounteService = accounteService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUp command)
        { 
            await _accounteService.SignUpAsync(command.Name, command.FullName, command.Email, 
                command.Password, command.Avatar, command.Profession, command.Role);

            return NoContent();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command)
            => Ok(await _accounteService.SignInAsync(command.Email, command.Password));

        [HttpPost("Refresh/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(refreshToken));

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword command)
        {
            await _accounteService.ChangePasswordAsync(command.UserId, command.CurrentPassword, command.NewPassword);
            
            return NoContent();
        }
    }
}