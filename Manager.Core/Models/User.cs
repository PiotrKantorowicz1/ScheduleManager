using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Manager.Core.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Manager.Core.Models
{
    public class User
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Profession { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Activity> ActivityCreated { get; set; }
        public ICollection<Schedule> SchedulesCreated { get; set; }
        public ICollection<Attendee> SchedulesAttended { get; set; }

        public User()
        {
            SchedulesCreated = new List<Schedule>();
            SchedulesAttended = new List<Attendee>();
            ActivityCreated = new List<Activity>();
        }

        public User(string name, string email, string fullName, string avatar,
            string role, string profession)
        {
            SetName(name);
            SetFullName(fullName);
            SetEmail(email);
            SetRole(role);
            SetAvatar(avatar);
            SetProfession(profession);
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetName(string username)
        {
            if (!NameRegex.IsMatch(username))
            {
                throw new DomainException(ErrorCodes.InvalidUsername,
                    "Username is invalid.");
            }

            if (String.IsNullOrEmpty(username))
            {
                throw new DomainException(ErrorCodes.InvalidUsername,
                    "Username is invalid.");
            }

            Name = username.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetFullName(string fullName)
        {
            if (fullName.Length > 100)
            {
                throw new DomainException(ErrorCodes.InvaliFullName,
                    "Fullname cannot be longer than 100 characters");
            }

            FullName = fullName;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new DomainException(ErrorCodes.InvalidEmail,
                    "Email can not be empty.");
            }
            if (Email == email)
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new DomainException(ErrorCodes.InvalidRole,
                    "Role can not be empty.");
            }
            if (Role == role)
            {
                return;
            }

            Role = role;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAvatar(string avatar)
        {
            if (string.IsNullOrEmpty(avatar))
            {
                throw new DomainException(ErrorCodes.InvalidAvatar,
                    "User must have avatar.");
            }

            Avatar = avatar;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetProfession(string profession)
        {
            if (string.IsNullOrEmpty(profession))
            {
                throw new DomainException(ErrorCodes.InvaliProfession,
                    "Profession cannot be empty.");
            }
            if (profession.Length > 1000)
            {
                throw new DomainException(ErrorCodes.InvaliProfession,
                    "Profession field cannot be longer than 100 characters.");
            }
             
            Profession = profession;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password can not be empty.");
            }

            Password = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, Password, password) != PasswordVerificationResult.Failed;

    }
}