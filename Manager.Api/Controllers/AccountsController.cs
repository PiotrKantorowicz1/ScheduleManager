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

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command)
            => Ok(await _accounteService.SignInAsync(command.Email, command.Password));
    }
}