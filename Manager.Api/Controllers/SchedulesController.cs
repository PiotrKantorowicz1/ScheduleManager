using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Struct.DTO;

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

        public async Task<IActionResult> Get()
        {
            var schedules = await _scheduleSerivce.BrowseAsync();

            return Json(schedules);
        }

        [HttpGet("{id}", Name = "GetSchedule")]
        public async Task<IActionResult> Get(int id)
        {
            var schedule = await _scheduleSerivce.GetAsync(id);

            if (schedule == null)
            {
                NotFound();
            }

            return Json(schedule);
        }

        [HttpGet("{id}/details", Name = "GetScheduleDetails")]
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
        public async Task<IActionResult> Create(ScheduleDto schedule)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSchedule = await _scheduleSerivce.CreateAsync(schedule);

            return Json(newSchedule);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _scheduleSerivce.EditAsync(id);

            return NoContent();
        }

        [HttpDelete("{id}", Name = "RemoveSchedule")]
        public async Task<IActionResult> Delete(int id)
        {
            await _scheduleSerivce.DeleteAsync(id);

            return NoContent();

        }

        [HttpDelete("{id}/removeattendee/{attendee}")]
        public async Task<IActionResult> Delete(int id, int attendee)
        {
            await _scheduleSerivce.DeleteAttendeesAsync(id, attendee);

            return NoContent();

        }
    }
}