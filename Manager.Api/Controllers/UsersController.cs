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

        [HttpGet]
        [Route("GetAllPageable")]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAllPegeable();

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

        [HttpGet]
        [Route("{id}/Schedules")]
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
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = await _userService.RegisterAsync(user.Name, user.Email, user.FullName,
                user.Password, user.Avatar, user.Role, user.Profession);

            return Json(newUser);
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UserDto user)
        {
            await _userService.UpdateUserAsync(id, user);

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


