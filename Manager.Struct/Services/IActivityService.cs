using System;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Queries.Activities;
using Manager.Core.Types;
using Manager.Struct.DTO;

namespace Manager.Struct.Services
{
    public interface IActivityService : IService
    {
        Task<ActivityDto> GetAsync(int id);
        Task<ActivityDetailsDto> GetDetailsAsync(int id);
        Task<PagedResult<ActivityDto>> GetAllPageable();
        Task<PagedResult<ActivityDto>> FilterByCreator(BrowseActivitiesByCreator query);
        Task<PagedResult<ActivityDto>> FilterByTitle(BrowseActivitiesByTitle query);
        Task<PagedResult<ActivityDto>> FilterByLocation(BrowseActivitiesByLocation query);
        Task CreateAsync(string title, string description, DateTime timestart, DateTime timeEnd,
            string location, int creatorId, ActivityType type, ActivityPriority priority, ActivityStatus status);
        Task UpdateAsync(int id, string title, string description, DateTime timestart, DateTime timeEnd,
            string location, int creatorId, ActivityType type, ActivityPriority priority, ActivityStatus status);
        Task DeleteAsync(int id);
    }
}