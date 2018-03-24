using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Manager.Api.Controllers
{
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accounteService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AccountsController(IAccountService accounteService, IRefreshTokenService refreshTokenService,
            ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
            _accounteService = accounteService;
            _refreshTokenService = refreshTokenService;
        }

        [AllowAnonymous]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUp command)
        { 
            await DispatchAsync(command);

            return Created($"users/'{command.Email}'", null);
        }

        [AllowAnonymous]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignIn command)
            => Ok(await _accounteService.SignInAsync(command.Email, command.Password));

        [HttpPost("Refresh/{refreshToken}")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
            => Ok(await _refreshTokenService.CreateAccessTokenAsync(refreshToken));

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword command)
        {
            await DispatchAsync(command);
            
            return Content($"Password from user with id: '{command.UserId}' was changed successfully.");
        }

        [HttpPost("Revoke")]
        public async Task<IActionResult> Revoke([FromBody] Revoke command)
        {
            await DispatchAsync(command);
            
            return Content($"Successfully logout user with id: '{command.UserId}'");
        }

        [HttpPut("ChangeRole")]
        public async Task<IActionResult> ChangeRole([FromBody] ChangeRole command)
        {
            await DispatchAsync(command);

            return Content($"Role from user with id: '{command.Id}' was changed succesfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {         
            await DispatchAsync(new DeleteAccount(id));

            return Content($"Successfully deleted user with id: {id}");
        }

    }
}