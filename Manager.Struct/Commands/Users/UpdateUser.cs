namespace Manager.Struct.Commands.Users
{
    public class UpdateUser : ICommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string Profession { get; set; }
    }
}