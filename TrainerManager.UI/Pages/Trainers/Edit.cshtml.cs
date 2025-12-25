using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Application.Features.Trainers.Queries;
using TrainerManager.Application.Features.Trainers.DTOs;

namespace TrainerManager.UI.Pages.Trainers
{
    public class EditModel(IMediator mediator) : PageModel
    {
        [BindProperty]
        public UpdateTrainerCommand Command { get; set; } = new();

        // This property allows the UI to show the existing photo while editing
        public string? CurrentProfileImagePath { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var trainer = await mediator.Send(new GetTrainerByIdQuery(id));
            if (trainer == null) return NotFound();

            // Store existing image path for the UI preview
            CurrentProfileImagePath = trainer.ProfileImagePath;

            // Map DTO to Command for the form
            // Ensure all fields from TrainerDetailsDto are mapped to Command properties
            Command = new UpdateTrainerCommand
            {
                Id = trainer.Id,
                FirstName = trainer.FirstName,
                LastName = trainer.LastName,
                Email = trainer.Email,
                MobileNumber = trainer.MobileNumber, // Added
                LinkedInUrl = trainer.LinkedInUrl,   // Added
                IdentityNumber = trainer.IdentityNumber, // Added

                // Professional Details
                TechField = trainer.Field,
                Specialization = trainer.Specialization,
                Experience = trainer.YearsOfExperience,
                LastCompanyName = trainer.LastCompanyName,

                // Location
                Street = trainer.Address.Street,
                City = trainer.Address.City,
                State = trainer.Address.State,
                Zip = trainer.Address.Zip,
                Country = trainer.Address.Country,

                // Visa Information
                VisaType = trainer.VisaType,       // Added
                VisaCountry = trainer.VisaCountry, // Added
                VisaExpiry = trainer.VisaExpiry,   // Added

                // Financials
                Rate = trainer.Costing.HourlyRate,
                Currency = trainer.Costing.Currency,
                BankName = trainer.AccountDetails?.BankName,
                AccountNumber = trainer.AccountDetails?.AccountNumber,

                // Certifications
                Certifications = trainer.Certifications.Select(c => new CertificationDto
                {
                    Name = c.Name,
                    IssuingOrganization = c.IssuingOrganization,
                    DateObtained = c.DateObtained
                }).ToList()
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // If validation fails, we must re-fetch the image path 
                // so the preview doesn't break when the page reloads
                var trainer = await mediator.Send(new GetTrainerByIdQuery(Command.Id));
                CurrentProfileImagePath = trainer?.ProfileImagePath;

                return Page();
            }

            var success = await mediator.Send(Command);

            if (success)
            {
                return RedirectToPage("./Details", new { id = Command.Id });
            }

            ModelState.AddModelError(string.Empty, "An error occurred while saving the trainer.");
            return Page();
        }
    }
}