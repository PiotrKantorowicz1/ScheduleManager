using System;
using Manager.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Manager.Core.Models
{
    public class RefreshToken
    {
        public Guid Id { get; protected set; }
        public int UserId { get; protected set; }
        public string Token { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? RevokedAt { get; protected set; }
        public bool Revoked => RevokedAt.HasValue;

        protected RefreshToken()
        {
        }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            Id = Guid.NewGuid();
            UserId = user.Id;
            CreatedAt = DateTime.UtcNow;
            Token = passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
        }

        public void Revoke()
        {
            if (Revoked)
            {
                throw new DomainException(ErrorCodes.InvalidRefreshToken,
                    $"Refresh token: '{Id}' was already revoked at '{RevokedAt}'.");
            }
            RevokedAt = DateTime.UtcNow;
        }
    }
}