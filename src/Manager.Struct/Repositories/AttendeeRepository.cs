using Manager.Core.Models;
using Manager.Struct.EF;
using Manager.Core.Repositories;

namespace Manager.Struct.Repositories
{
    public class AttendeeRepository : RepositoryBase<Attendee>, IAttendeeRepository, ISqlRepository
    {
        public AttendeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}