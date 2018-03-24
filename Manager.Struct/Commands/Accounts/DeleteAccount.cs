using System;

namespace Manager.Struct.Commands.Accounts
{
    public class DeleteAccount : ICommand
    {
        public int Id { get; set; }

        public DeleteAccount(int id)
        {
            Id = id;
        }
    }
}
