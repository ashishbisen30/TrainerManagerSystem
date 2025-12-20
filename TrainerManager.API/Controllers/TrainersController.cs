using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Application.Features.Trainers.Queries;

namespace TrainerManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)] // Ensures API always speaks JSON
    public class TrainersController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateTrainerCommand cmd)
            => Ok(await mediator.Send(cmd));

        [HttpGet]
        // Adding the response type helps Swagger display the schema for your PaginatedResponse
        [ProducesResponseType(typeof(PaginatedResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] GetTrainersQuery? query)
        {
            // If query is null (rare but possible), use default values
            var result = await mediator.Send(query ?? new GetTrainersQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TrainerSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TrainerSummaryDto>> GetById(int id)
        {
            try
            {
                var result = await mediator.Send(new GetTrainerByIdQuery(id));
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        // <summary>
        /// Update an existing trainer's details.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TrainerSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TrainerSummaryDto>> Update(int id, [FromBody] UpdateTrainerCommand command)
        {
            // Security check: Ensure the ID in the URL matches the ID in the body
            if (id != command.Id)
            {
                return BadRequest("ID mismatch between URL and request body.");
            }

            try
            {
                var result = await mediator.Send(command);
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
            => Ok(await mediator.Send(new DeleteTrainerCommand(id)));
    }
}