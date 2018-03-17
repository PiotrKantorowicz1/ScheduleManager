﻿using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface  IAccountService : IService
    {
        Task SignUpAsync(string name, string fullName, string email, string password,
            string avatar, string profession, string role = Roles.User);
        Task<JsonWebToken> SignInAsync(string email, string password);
        Task ChangePasswordAsync(int userId, string currentPassword, string newPassword);
    }
}