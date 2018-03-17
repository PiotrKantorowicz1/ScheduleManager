namespace Manager.Struct.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        int UserId { get; set; }
    }
}