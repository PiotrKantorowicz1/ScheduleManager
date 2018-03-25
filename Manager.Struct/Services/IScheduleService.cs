using System;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Queries.Schedules;
using Manager.Core.Types;
using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface IScheduleService : IService
    {
        Task<ScheduleDto> GetAsync(int id);
        Task<ScheduleDetailsDto> GetScheduleDetailsAsync(int id);
        Task<PagedResult<ScheduleDto>> BrowseAsync();
        Task<PagedResult<ScheduleDto>> BrowseByCreatorAsync(BrowseSchedulesByCreator query);
        Task<PagedResult<ScheduleDto>> BrowseByTitleAsync(BrowseSchedulesByTitle query);
        Task CreateAsync(int id, string title, string description, DateTime timestart, DateTime timeEnd,
            string location, int creatorId, string type, string status);
        Task UpdateAsync(int id, string title, string description, DateTime timeStart, DateTime timeEnd,
            string location, int creatorId, string type, string status);
        Task DeleteAsync(int id);
        Task DeleteAttendeesAsync(int id, int attendeeId);
    }
}