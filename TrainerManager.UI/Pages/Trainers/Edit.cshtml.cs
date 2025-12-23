using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Application.Features.Trainers.Queries;

namespace TrainerManager.UI.Pages.Trainers
{
    public class EditModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public UpdateTrainerCommand Command { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var trainer = await mediator.Send(new GetTrainerByIdQuery(id));
            if (trainer == null) return NotFound();

            // Map DTO to Command for the form
            Command = new UpdateTrainerCommand
            {
                Id = trainer.Id,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Email = trainer.Email,
                Field = trainer.Field,
                Specialization = trainer.Specialization,
                Experience = trainer.YearsOfExperience,
                LastCompanyName = trainer.LastCompanyName,
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                State = trainer.Address.State,
                Zip = trainer.Address.Zip,
                Country = trainer.Address.Country,
                Rate = trainer.Costing.HourlyRate,
                Currency = trainer.Costing.Currency,
                BankName = trainer.AccountDetails.BankName,
                AccountNumber = trainer.AccountDetails.AccountNumber
            };
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var success = await mediator.Send(Command);
            return success ? RedirectToPage("./Details", new { id = Command.Id }) : NotFound();
        }
    }
}
