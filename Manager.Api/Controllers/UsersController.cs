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
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateUser command)
        {
           await DispatchAsync(command);

           return NoContent();
        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public IActionResult Delete(int id)
        {
            _userService.RemoveUserAsync(id);

            return NoContent();
        }
    }
}


