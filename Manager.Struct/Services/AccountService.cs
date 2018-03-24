using System;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.DTO;
using Manager.Struct.EF;
using Manager.Struct.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Manager.Struct.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IJwtHandler _jwtHandler;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task SignUpAsync(Guid serialNumber, string name, string fullName, string email, string password,
            string avatar, string profession, string role = Roles.User)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                throw new ServiceException(ErrorCodes.EmailInUse,
                    $"Email: '{email}' is already in use.");
            }
            user = new User(serialNumber, name, email, fullName, avatar, role, profession);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<JsonWebToken> SignInAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !user.ValidatePassword(password, _passwordHasher))
            {
                throw new ServiceException(ErrorCodes.InvalidCredentials,
                    "Invalid credentials.");
            }
            var refreshToken = new RefreshToken(user, _passwordHasher);
            var jwt = _jwtHandler.CreateToken(user.SerialNumber, user.Role);
            jwt.RefreshToken = refreshToken.Token;
            await _refreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return jwt;
        }

        public async Task ChangePasswordAsync(int userId, string currentPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound, 
                    $"User: '{userId}' was not found.");
            }
            if (!user.ValidatePassword(currentPassword, _passwordHasher))
            {
                throw new ServiceException(ErrorCodes.InvalidCredentials, 
                    "Invalid current password.");
            }
            user.SetPassword(newPassword, _passwordHasher);
            _userRepository.Update(user);    
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task ChangeRoleAsync(int userId, string role)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {                            
                throw new ServiceException(ErrorCodes.UserNotFound, 
                    $"User: '{userId}' was not found.");         
            }
            user.SetRole(role);
            _userRepository.Update(user);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
