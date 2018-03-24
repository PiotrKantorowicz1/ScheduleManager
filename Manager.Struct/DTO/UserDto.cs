using System;

namespace Manager.Struct.DTO
{
     public class UserDto 
     {
        public int Id { get; set; }
        public Guid SerialNumber { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Role { get; set; }
        public string Profession { get; set; }
    }
}
