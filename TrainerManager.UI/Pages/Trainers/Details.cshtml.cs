using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Application.Features.Trainers.Queries;

namespace TrainerManager.UI.Pages.Trainers
{
    //public class DetailsModel : PageModel
    //{
    //    public void OnGet()
    //    {
    //    }
    //}

    public class DetailsModel : PageModel
    {
        private readonly IMediator _mediator;

        public DetailsModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        // The DTO that holds the trainer data
        public TrainerSummaryDto Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // We call the Mediator Query directly
            var result = await _mediator.Send(new GetTrainerByIdQuery(id));

            if (result == null)
            {
                return NotFound();
            }

            Trainer = result;
            return Page();
        }
    }
}
