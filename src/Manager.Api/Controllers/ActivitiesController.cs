using Microsoft.AspNetCore.Mvc;
using Manager.Struct.Services;
using System.Threading.Tasks;
using Manager.Core.Models;
using Manager.Core.Queries.Activities;
using Manager.Struct.Commands;
using Manager.Struct.DTO;
using Manager.Api.Framework;
using Manager.Struct.Commands.Activities;

namespace Manager.Api.Controllers
{
    public class ActivitiesController : BaseController
    {
        private readonly IActivityService _activityService;

        public ActivitiesController(IActivityService activityService, ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            _activityService = activityService;
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(int id)
            => Single(await _activityService.GetAsync(id), x => x.Id == id || IsAdmin);

        [HttpGet("{id}/Details")]
        public async Task<IActionResult> GetTaskDetails(int id)
            => Single(await _activityService.GetDetailsAsync(id));

        [AdminAuth]
        [HttpGet("GetAllPageable")]
        public async Task<IActionResult> GetAllPageable()
            => Collection(await _activityService.BrowseAsync());

        [HttpGet("FilterByCreator")]
        public async Task<IActionResult> Get([FromQuery] BrowseActivitiesByCreator query)
            => Collection(await _activityService.BrowseByCreatorAsync(query));

        [AdminAuth]
        [HttpGet("FilterByTitle")]
        public async Task<IActionResult> Get([FromQuery] BrowseActivitiesByTitle query)
            => Collection(await _activityService.BrowseByTitleAsync(query));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateActivity command)
        {
            await DispatchAsync(command);
            return Content($"Successfully created activity with title: '{command.Title}'");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] UpdateActivity command)
        {
            await DispatchAsync(command);
            return Content($"Successfully updated activity with title: '{command.Title}'");
        }

        [HttpDelete("Remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await DispatchAsync(new DeleteActivity(id));
            return NoContent();
        }
    }
}