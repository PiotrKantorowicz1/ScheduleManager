namespace Manager.Struct.Commands.Accounts
{
    public class SignIn : ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
