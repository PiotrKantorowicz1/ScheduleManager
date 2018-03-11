using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Queries.Schedules;

namespace Manager.Api.Controllers
{
    [Route("api/[controller]")]
    public class SchedulesController : Controller
    {
        private readonly IScheduleService _scheduleSerivce;

        public SchedulesController(IScheduleService scheduleSerivce)
        {
            _scheduleSerivce = scheduleSerivce;
        }

        [HttpGet]
        [Route("GetAllPageable")]
        public async Task<IActionResult> Get()
        {
            var activity = await _scheduleSerivce.BrowseAsync();

            return Json(activity);
        }

        [HttpGet]
        [Route("FilterByCreator/{creatorId}")]
        public async Task<IActionResult> Get(BrowseSchedulesByCreator query)
        {
            var users = await _scheduleSerivce.BrowseByCreatorAsync(query);

            return Json(users);
        }

        [HttpGet]
        [Route("FilterByTitle/{title}")]
        public async Task<IActionResult> Get(BrowseSchedulesByTitle query)
        {
            var users = await _scheduleSerivce.BrowseByTitleAsync(query);

            return Json(users);
        }

        [HttpGet]
        [Route("FilterByLocation/{location}")]
        public async Task<IActionResult> Get(BrowseSchedulesByLocation query)
        {
            var users = await _scheduleSerivce.BrowseByLocationAsync(query);

            return Json(users);
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var schedule = await _scheduleSerivce.GetAsync(id);

            if (schedule == null)
            {
                NotFound();
            }

            return Json(schedule);
        }

        [HttpGet]
        [Route("{id}/Details")]
        public async Task<IActionResult> GetScheduleDetails(int id)
        {
            var scheduleDetails = await _scheduleSerivce.GetScheduleDetailsAsync(id);

            if (scheduleDetails == null)
            {
                NotFound();
            }

            return Json(scheduleDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Schedule schedule)
        {
            await _scheduleSerivce.CreateAsync(schedule.Id, schedule.Title, schedule.Description, schedule.TimeStart, 
                schedule.TimeEnd, schedule.Location, schedule.CreatorId, schedule.Type, schedule.Status);

            return Created($"users/{schedule.Title}", null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Schedule schedule)
        {
            await _scheduleSerivce.UpdateAsync(id, schedule.Title, schedule.Description, schedule.TimeStart,
                schedule.TimeEnd, schedule.Location, schedule.CreatorId, schedule.Type, schedule.Status);

            return NoContent();
        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _scheduleSerivce.DeleteAsync(id);

            return NoContent();
        }

        [HttpDelete]
        [Route("Remove/{id}/Attendee/{id}")]
        public async Task<IActionResult> Delete(int id, int attendeeId)
        {
            await _scheduleSerivce.DeleteAttendeesAsync(id, attendeeId);

            return NoContent();

        }
    }
}