using System;

namespace Manager.Struct.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        Guid SerialNumber { get; set; }
    }
}