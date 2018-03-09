using Microsoft.AspNetCore.Mvc;
using Manager.Struct.DTO;
using Manager.Struct.Services;
using System.Threading.Tasks;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IScheduleService _scheduleService;

        public UsersController(IUserService userService, IScheduleService scheduleService)
        {
            _userService = userService;
            _scheduleService = scheduleService;
        }


        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            var users = await _userService.BrowseAsync();
            var pagedUsers = CreatePagedResults(users, page - 1, pageSize);

            return Json(pagedUsers);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _userService.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpGet("{id}/schedules", Name = "GetUserSchedules")]
        public async Task<IActionResult> GetSchedulesAsync(int id, int page = 1, int pageSize = 10)
        {
            var userSchedules = await _scheduleService.GetSchedulesAsync(id);
            var pagedUserSchedules = CreatePagedResults(userSchedules, page - 1, pageSize);

            if (userSchedules == null)
            {
                NotFound();
            }

            return Json(pagedUserSchedules);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = await _userService.RegisterAsync(user.Name, user.Avatar, user.Profession);

            return Json(newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserDto user)
        {
            await _userService.EditAsync(id, user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _userService.DeleteAsync(id);

            return NoContent();
        }
    }
}


