using System;
using Manager.Core.Models;

namespace Manager.Struct.Commands.Schedules
{
    public class DeleteSchedule : ICommand
    {
        public int Id { get; set; }

        public DeleteSchedule(int id)
        {
            Id = id;
        }
    }
}
