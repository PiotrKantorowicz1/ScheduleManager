using System.Collections.Generic;
using Manager.Struct.DTO;
using System.Threading.Tasks;
using Manager.Core.Queries.Users;
using Manager.Core.Types;
using System;

namespace Manager.Struct.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(int id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<Guid> GetSerialNumerAsync(string email);
        Task<string> GetUserRoleAsync(string email);
        Task<bool> IsUserInRoleAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<PagedResult<UserDto>> BrowseAsync();
        Task<PagedResult<UserDto>> BrowseByRoleAsync(BrowseUsersByRole query);
        Task UpdateUserAsync(int id, string name, string email, string fullName,
           string avatar, string role, string profession);
        Task RemoveUserActivitiesAsync(int id);
        Task DeleteUserActivitiesProperlyAsync(int id);
        Task RemoveUserSchedulesAsync(int id);
        Task DeleteUserSchedulesProperlyAsync(int id);
        Task RemoveUserAttendeesAsync(int id);
        Task DeleteUserAttendeesProperlyAsync(int id);
    }
}