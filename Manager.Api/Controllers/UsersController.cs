using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Core.Queries.Users;
using Manager.Struct.Commands.Users;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("GetAllPageable")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.BrowseAsync();

            return Json(users);
        }

        [HttpGet]
        [Route("FilterByProfession/{profession}")]
        public async Task<IActionResult> Get(BrowseUsersByProfession query)
        {
            var users = await _userService.BrowseByProfessionAsync(query);

            return Json(users);
        }

        [HttpGet]
        [Route("FilterByRole/{role}")]
        public async Task<IActionResult> Get(BrowseUsersByRole query)
        {
            var users = await _userService.BrowseByRoleAsync(query);

            return Json(users);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update([FromBody] UpdateUser command)
        {
           await _userService.UpdateUserAsync(command.Id, command.Name, command.FullName, command.Email,
                command.Avatar, command.Role, command.Profession);

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


