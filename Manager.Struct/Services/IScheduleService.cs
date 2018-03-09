using System.Collections.Generic;
using System.Threading.Tasks;
using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface IScheduleService : IService
    {
        Task<ScheduleDto> GetAsync(int id);
        Task<ScheduleDetailsDto> GetScheduleDetailsAsync(int id);
        Task<IEnumerable<ScheduleDto>> BrowseAsync();
        Task<IEnumerable<ScheduleDto>> GetSchedulesAsync(int id);
        Task<ScheduleDto> CreateAsync(ScheduleDto schedule);
        Task<ScheduleDto> EditAsync(int id);
        Task DeleteAsync(int id);
        Task DeleteAttendeesAsync(int id, int attendee);
    }
}