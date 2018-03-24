using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Core.Queries.Users;
using Manager.Struct.Commands.Users;
using Manager.Struct.Commands;
using Manager.Api.Framework;

namespace Manager.Api.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
            => Single(await _userService.GetAsync(id), x => x.Id == id || IsAdmin);

        [HttpGet]
        [Route("Get/{email}")]
        public async Task<IActionResult> Get(string email)
            => Single(await _userService.GetByEmailAsync(email), x => x.Email == email || IsAdmin);

        [HttpGet]
        [AdminAuth]
        [Route("GetSerialNumber/{email}")]
        public async Task<IActionResult> GetSerialNumber(string email)
            => Single(await _userService.GetSerialNumerAsync(email));

        [HttpGet]
        [Route("GetRole/{email}")]
        public async Task<IActionResult> GetRole(string email)
            => Single(await _userService.GetUserRoleAsync(email));

        [HttpGet]
        [Route("GetInRole/{email}")]
        public async Task<IActionResult> IsUserInRole(string email)
            => Single(await _userService.IsUserInRoleAsync(email));

        [HttpGet]
        [AdminAuth]
        [Route("GetAllPageable")]
        public async Task<IActionResult> GetAllPageable()
            => Collection(await _userService.BrowseAsync());

        [HttpGet]
        [AdminAuth]
        [Route("FilterByUserRole")]
        public async Task<IActionResult> FilterByUserRole([FromQuery] BrowseUsersByRole query)
            => Collection(await _userService.BrowseByRoleAsync(query));

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateUser command)
        {
            await DispatchAsync(command);

            return Content($"Succesfully updated user with Name: '{command.Name}'");
        }

        [HttpDelete]
        [AdminAuth]
        [Route("Activities/{id}")]
        public async Task<IActionResult> DeleteActivities(int id)
        {
            await DispatchAsync(new RemoveUserActivities(id));

            return Content($"Successfully delete activitivies");
        }

        [HttpDelete]
        [AdminAuth]
        [Route("Schedules/{id}")]
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            await DispatchAsync(new RemoveUserSchedules(id));

            return Content($"Successfully delete schedules");
        }

        [HttpDelete]
        [AdminAuth]
        [Route("Attendees/{id}")]
        public async Task<IActionResult> DeleteAttendees(int id)
        {
            await DispatchAsync(new RemoveUserAttendees(id));

            return Content($"Successfully delete attendees");
        }
    }
}


