using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Queries.Schedules;
using Manager.Struct.Commands;
using Manager.Struct.Commands.Schedules;
using Manager.Api.Framework;

namespace Manager.Api.Controllers
{
    public class SchedulesController : BaseController
    {
        private readonly IScheduleService _scheduleSerivce;

        public SchedulesController(IScheduleService scheduleSerivce,
            ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _scheduleSerivce = scheduleSerivce;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
            => Single(await _scheduleSerivce.GetAsync(id), x => x.Id == id || IsAdmin);

        [HttpGet("{id}/Details")]
        public async Task<IActionResult> GetScheduleDetails(int id)
            => Single(await _scheduleSerivce.GetScheduleDetailsAsync(id), x => x.Id == id || IsAdmin);

        [AdminAuth]
        [HttpGet("GetAllPageable")]
        public async Task<IActionResult> GetAllPageable()
            => Collection(await _scheduleSerivce.BrowseAsync());

        [HttpGet("FilterByCreator")]
        public async Task<IActionResult> FilterByCreator([FromQuery]BrowseSchedulesByCreator query)
            => Collection(await _scheduleSerivce.BrowseByCreatorAsync(query));

        [AdminAuth]
        [HttpGet("FilterByTitle")]
        public async Task<IActionResult> FilterByTitle([FromQuery]BrowseSchedulesByTitle query)
            => Collection(await _scheduleSerivce.BrowseByTitleAsync(query));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]CreateSchedule command)
        {
            await DispatchAsync(command);
            return Content($"Successfully created schedule with title: '{command.Title}'");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put(UpdateSchedule command)
        {
            await DispatchAsync(command);
            return Content($"Successfully updated schedule with title: '{command.Title}'");
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await DispatchAsync(new DeleteSchedule(id));
            return Content($"Successfully deleted schedule with id: '{id}'");
        }

        [HttpDelete("Remove/{scheduleId}/Attendee/{attendeeId}")]
        public async Task<IActionResult> Delete(int scheduleId, int attendeeId)
        {
            await DispatchAsync(new DeleteScheduleAttendees(scheduleId, attendeeId));
            return NoContent();
        }
    }
}