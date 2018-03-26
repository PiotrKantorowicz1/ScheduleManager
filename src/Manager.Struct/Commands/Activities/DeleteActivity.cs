using System;
using Manager.Core.Models;

namespace Manager.Struct.Commands.Activities
{
    public class DeleteActivity : ICommand
    {
        public int Id { get; set; }

        public DeleteActivity(int id)
        {
            Id = id;
        }
    }
}
