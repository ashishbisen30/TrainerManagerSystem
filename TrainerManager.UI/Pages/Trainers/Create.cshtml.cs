using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainerManager.Application.Features.Trainers.Commands;

namespace TrainerManager.UI.Pages.Trainers
{
    public class CreateModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public CreateTrainerCommand Command { get; set; } = new();

        public void OnGet()
        {
            // Initializes the form
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            try
            {
                await mediator.Send(Command);
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                // This will catch database or file saving errors
                ModelState.AddModelError(string.Empty, $"Internal error: {ex.Message}");
                return Page();
            }
            //// Sends the command to the Application Layer via MediatR
            //await mediator.Send(Command);

            //return RedirectToPage("Index");
        }
    }
}
