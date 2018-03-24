using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Repositories;
using Manager.Struct.DTO;
using Manager.Struct.EF;
using Manager.Struct.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Manager.Struct.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IJwtHandler _jwtHandler;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository, IUserRepository userRepository, IJwtHandler jwtHandler,
            IPasswordHasher<User> passwordHasher, IUnitOfWork unitOfWork)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _jwtHandler = jwtHandler;
            _passwordHasher = passwordHasher;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(int userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User: '{userId}' was not found.");
            }
            await _refreshTokenRepository.CreateTokenAsync(new RefreshToken(user, _passwordHasher));
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<JsonWebToken> CreateAccessTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null)
            {
                throw new ServiceException(ErrorCodes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }
            if (refreshToken.Revoked)
            {
                throw new ServiceException(ErrorCodes.RefreshTokenAlreadyRevoked,
                    $"Refresh token: '{refreshToken.Id}' was revoked.");
            }
            var user = await _userRepository.GetAsync(refreshToken.UserId);
            if (user == null)
            {
                throw new ServiceException(ErrorCodes.UserNotFound,
                    $"User: '{refreshToken.UserId}' was not found.");
            }
            var jwt = _jwtHandler.CreateToken(user.SerialNumber, user.Role);
            jwt.RefreshToken = refreshToken.Token;

            return jwt;
        }

        public async Task RevokeAsync(string token, int userId)
        {
            var refreshToken = await _refreshTokenRepository.GetAsync(token);
            if (refreshToken == null || refreshToken.UserId != userId)
            {
                throw new ServiceException(ErrorCodes.RefreshTokenNotFound,
                    "Refresh token was not found.");
            }
            refreshToken.Revoke();
            _refreshTokenRepository.Update(refreshToken);
            await _refreshTokenRepository.Commit();
        }
    }
}
