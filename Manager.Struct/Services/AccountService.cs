using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.DTO;
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

        public AccountService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
            IJwtHandler jwtHandler, IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtHandler = jwtHandler;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task SignUpAsync(string name, string fullName, string email, string password,
            string avatar, string profession, string role = Roles.User)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null)
            {
                throw new ServiceException(ErrorCodes.EmailInUse,
                    $"Email: '{email}' is already in use.");
            }
            user = new User(name, email, fullName, avatar, role, profession);
            user.SetPassword(password, _passwordHasher);
            await _userRepository.AddAsync(user);
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
            var jwt = _jwtHandler.CreateToken(user.Id, user.Role);
            jwt.RefreshToken = refreshToken.Token;

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
            await _userRepository.UpdateAsync(user);        
        }
    }
}
