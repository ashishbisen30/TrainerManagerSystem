using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using TrainerManager.Domain.Entities;
using TrainerManager.Domain.ValueObjects;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

namespace TrainerManager.Application.Features.Trainers.Commands
{
    public record CreateTrainerCommand : IRequest<int>
    {
        // Basic Info
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string? Email { get; set; }

        // Address (Value Object)
        // Made these nullable or defaulted to prevent Validation errors
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        // Costing (Value Object)
        public decimal Rate { get; set; }
        public string Currency { get; set; } = "INR";

        // Account Details (Owned Entity)
        public string? BankName { get; set; } = string.Empty;
        public string? AccountNumber { get; set; } = string.Empty;

        // Additional Fields
        public string? TechField { get; set; } = string.Empty;
        public string? Specialization { get; set; } = string.Empty;
        public int Experience { get; set; } 
        public string? LastCompanyName { get; set; } = string.Empty;

        //public int Experience { get; set; }
        //public string? TechField { get; set; }

        // Files
        public IFormFile? ProfileImage { get; set; }
        public IFormFile? ResumeFile { get; set; }
        
        //public IFormFile? ProfileImage { get; set; }
        //public IFormFile? ResumeFile { get; set; }
    }

    //{
    //    //public string? FirstName { get; set; }
    //    //public string? LastName { get; set; }
    //    //public string? Email { get; set; }
    //    //public string? City { get; set; }
    //    //public string? Country { get; set; }
    //    //public decimal Rate { get; set; }
    //    //public string? Currency { get; set; }
    //    //public int Experience { get; set; }
    //    //public string? TechField { get; set; }
    //    //public IFormFile? ProfileImage { get; set; }
    //    //public IFormFile? ResumeFile { get; set; }

    //    /*
    //    [Required] public string? FirstName { get; set; }
    //    [Required] public string? LastName { get; set; }
    //    [Required, EmailAddress] public string? Email { get; set; }

    //    // Address Details (Required by your Migration)
    //    [Required] public string Street { get; set; } = "N/A";
    //    [Required] public string City { get; set; } = "N/A";
    //    [Required] public string State { get; set; } = "N/A";
    //    [Required] public string Zip { get; set; } = "000000";
    //    [Required] public string Country { get; set; } = "India";

    //    // Costing (Required by your Migration)
    //    [Required] public decimal Rate { get; set; }
    //    [Required] public string Currency { get; set; } = "INR";

    //    // Bank Details (Required by your Migration)
    //    [Required] public string AccountNumber { get; set; } = "Pending";
    //    [Required] public string BankName { get; set; } = "Pending";

    //    public int Experience { get; set; }
    //    public string? TechField { get; set; }
    //    public IFormFile? ProfileImage { get; set; }
    //    public IFormFile? ResumeFile { get; set; }
    //    */
    //    public string? FirstName { get; set; }
    //    public string? LastName { get; set; }
    //    public string? Email { get; set; }

    //    // Providing defaults ensures the Model is valid even if fields are empty
    //    public string Street { get; set; } = "Not Specified";
    //    public string City { get; set; } = "Not Specified";
    //    public string State { get; set; } = "Not Specified";
    //    public string Zip { get; set; } = "Not Specified";
    //    public string Country { get; set; } = "Not Specified";
    //    public decimal Rate { get; set; } = 0;
    //    public int Experience { get; set; } = 0;
    //    public string TechField { get; set; } = "General";

    //    public IFormFile? ProfileImage { get; set; }
    //    public IFormFile? ResumeFile { get; set; }
    //}

    public class CreateTrainerHandler(TrainerDbContext context, IFileStorageService files)
        : IRequestHandler<CreateTrainerCommand, int>
    {
        public async Task<int> Handle(CreateTrainerCommand request, CancellationToken ct)

        {
            var trainer = new Trainer
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                YearsOfExperience = request.Experience,
                Field = request.TechField,
                Specialization = request.Specialization,
                LastCompanyName = request.LastCompanyName,

                // Mapping Owned Entities / Value Objects
                // Handle Value Objects with Fallbacks to prevent SQLite NOT NULL errors
                Address = new Address(
                    request.Street ?? "N/A",
                    request.City ?? "N/A",
                    request.State ?? "N/A",
                    request.Zip ?? "N/A",
                    request.Country ?? "N/A"
                ),

                Costing = new TrainerCosting(
                    request.Rate,
                    request.Currency ?? "INR"
                ),

                AccountDetails = new TrainerAccount
                {
                    BankName = request.BankName ?? "N/A",
                    AccountNumber = request.AccountNumber ?? "N/A"
                },

                // File Uploads
                ProfileImagePath = request.ProfileImage != null
                    ? await files.SaveFileAsync(request.ProfileImage, "images")
                    : null,
                ResumePath = request.ResumeFile != null
                    ? await files.SaveFileAsync(request.ResumeFile, "resumes")
                    : null
            };

            context.Trainers.Add(trainer);
            await context.SaveChangesAsync(ct);
            return trainer.Id;
        }
        /*{
        var trainer = new Trainer
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Field = request.TechField,
            YearsOfExperience = request.Experience,

            // Map Address (satisfies Migration constraints)
            Address = new Address(
                request.Street,
                request.City,
                request.State,
                request.Zip,
                request.Country),

            // Map Costing (satisfies Migration constraints)
            Costing = new TrainerCosting(request.Rate, request.Currency),

            // Map Account Details (satisfies Migration constraints)
            AccountDetails = new TrainerAccount
            {
                AccountNumber = request.AccountNumber,
                BankName = request.BankName
            },

            // Handle Files
            ProfileImagePath = request.ProfileImage != null
                ? await files.SaveFileAsync(request.ProfileImage, "images")
                : null,
            ResumePath = request.ResumeFile != null
                ? await files.SaveFileAsync(request.ResumeFile, "resumes")
                : null
        };

        context.Trainers.Add(trainer);
        await context.SaveChangesAsync(ct);
        return trainer.Id;
    }*/
        //{

        //    var trainer = new Trainer
        //    {
        //        FirstName = request.FirstName,
        //        LastName = request.LastName,
        //        Email = request.Email,
        //        Address = new Address("", request.City ?? "Unknown", "", "", request.Country ?? "Unknown"),
        //        //Address = new Address("", request.City, "", "", request.Country),
        //        Costing = new TrainerCosting(request.Rate, request.Currency),
        //        YearsOfExperience = request.Experience,
        //        Field = request.TechField,
        //        ProfileImagePath = await files.SaveFileAsync(request.ProfileImage, "images"),
        //        ResumePath = await files.SaveFileAsync(request.ResumeFile, "resumes")
        //    };
        //    context.Trainers.Add(trainer);
        //    await context.SaveChangesAsync(ct);
        //    return trainer.Id;
        //}
    }
}
