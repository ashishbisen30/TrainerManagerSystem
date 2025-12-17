using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Application.Features.Trainers.Queries;

namespace TrainerManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateTrainerCommand cmd) => Ok(await mediator.Send(cmd));

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTrainersQuery query) => Ok(await mediator.Send(query));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => Ok(await mediator.Send(new DeleteTrainerCommand(id)));
    }
}
