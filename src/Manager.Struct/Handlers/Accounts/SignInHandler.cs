﻿using System.Threading.Tasks;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Accounts;
using Manager.Struct.Services;

namespace Manager.Struct.Handlers.Accounts
{
    public class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IAccountService _accountService;

        public SignInHandler(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task HandleAsync(SignIn command)
        {
            await _accountService.SignInAsync(command.Email, command.Password);
        }
    }
}