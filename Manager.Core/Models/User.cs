using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Manager.Core.Exceptions;

namespace Manager.Core.Models
{
    public class User
    {
        private static readonly Regex NameRegex = new Regex("^(?![_.-])(?!.*[_.-]{2})[a-zA-Z0-9._.-]+(?<![_.-])$");

        public User()
        {
            SchedulesCreated = new List<Schedule>();
            SchedulesAttended = new List<Attendee>();
            ActivityCreated = new List<Activity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string Avatar { get; set; }
        public string Profession { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Activity> ActivityCreated { get; set; }
        public ICollection<Schedule> SchedulesCreated { get; set; }
        public ICollection<Attendee> SchedulesAttended { get; set; }

        public User(int userId, string name, string email, string fullName, 
            string password,string avatar, string role, string salt, string profession)
        {
            Id = userId;
            SetName(name);
            SetFullName(fullName);
            SetPassword(password, salt);
            SetEmail(email);
            SetRole(role);
            SetAvatar(avatar);
            SetProfession(profession);
            CreatedAt = DateTime.UtcNow;
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

        public void SetPassword(string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password can not be empty.");
            }
            if (string.IsNullOrWhiteSpace(salt))
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Salt can not be empty.");
            }
            if (password.Length < 4)
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password must contain at least 4 characters.");
            }
            if (password.Length > 100)
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password can not contain more than 100 characters.");
            }
            if (Password == password)
            {
                return;
            }

            Password = password;
            Salt = salt;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAvatar(string avatar)
        {
            if (string.IsNullOrEmpty(avatar))
            {
                throw new DomainException(ErrorCodes.InvalidAvatar,
                    "User must have avatar.");
            }
            if (!avatar.Contains(".jpg") || !avatar.Contains(".png"))
            {
                throw new DomainException(ErrorCodes.InvalidAvatar,
                    "This avatar is not valid.");
            }

            Avatar = avatar;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetProfession(string profession)
        {
            if (string.IsNullOrEmpty(profession))
            {
                throw new DomainException(ErrorCodes.IvalidProfession,
                    "Profession cannot be empty.");
            }
            if (profession.Length > 1000)
            {
                throw new DomainException(ErrorCodes.IvalidProfession,
                    "Profession field cannot be longer than 100 characters.");
            }

            Profession = profession;
            UpdatedAt = DateTime.UtcNow;
        }

    }
}