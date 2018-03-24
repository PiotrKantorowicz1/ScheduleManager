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

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> Get(int id)
            => Single(await _activityService.GetAsync(id), x => x.Id == id || IsAdmin);

        [HttpGet]
        [Route("{id}/Details")]
        public async Task<IActionResult> GetTaskDetails(int id)
            => Single(await _activityService.GetDetailsAsync(id));

        [HttpGet]
        [AdminAuth]
        [Route("GetAllPageable")]
        public async Task<IActionResult> GetAllPageable()
            => Collection(await _activityService.BrowseAsync());

        [HttpGet]
        [AdminAuth]
        [Route("FilterByCreator")]
        public async Task<IActionResult> Get([FromQuery] BrowseActivitiesByCreator query)
            => Collection(await _activityService.BrowseByCreatorAsync(query));

        [HttpGet]
        [AdminAuth]
        [Route("FilterByTitle")]
        public async Task<IActionResult> Get([FromQuery] BrowseActivitiesByTitle query)
            => Collection(await _activityService.BrowseByTitleAsync(query));

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] CreateActivity command)
        {
            await DispatchAsync(command);   

            return Content($"Successfully created activity with title: '{command.Title}'");
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Put([FromBody] UpdateActivity command)
        {
            await DispatchAsync(command);

            return Content($"Successfully updated activity with title: '{command.Title}'");
        }

        [HttpDelete]
        [Route("Remove/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await DispatchAsync(new DeleteActivity(id));

            return Content($"Successfully deleted activity with id: '{id}'");
        }
    }
}