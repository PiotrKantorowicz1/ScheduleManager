using System.Threading.Tasks;
using Manager.Core.Models;
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

        [HttpPost("sign-in/{email}/{password}")]
        public async Task<IActionResult> SignIn(string email, string password)
            => Ok(await _accounteService.SignInAsync(email, password));
    }
}