namespace Manager.Struct.Commands.Accounts
{
    public class SignIn : ICommand
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Avatar { get; set; }
        public string Profession { get; set; }
    }
}
