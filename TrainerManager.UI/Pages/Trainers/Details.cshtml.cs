using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using TrainerManager.Application.Features.Trainers.Queries;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Domain.Entities; // Ensure this is imported

namespace TrainerManager.UI.Pages.Trainers
{
    public class DetailsModel(IMediator mediator) : PageModel
    {
        // Make sure this matches the new full DTO
        public TrainerDetailsDto Trainer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await mediator.Send(new GetTrainerByIdQuery(id));
            if (result == null) return NotFound();

            Trainer = result;
            return Page();
        }
    }

    //public class DetailsModel(IMediator mediator) : PageModel
    //{
    //    // Change the type here from Trainer to TrainerSummaryDto
    //    public TrainerSummaryDto Trainer { get; set; } = default!;

    //    public async Task<IActionResult> OnGetAsync(int id)
    //    {
    //        var result = await mediator.Send(new GetTrainerByIdQuery(id));

    //        if (result == null)
    //        {
    //            return NotFound();
    //        }

    //        Trainer = result; // Now the types match!
    //        return Page();
    //    }
    //}
}