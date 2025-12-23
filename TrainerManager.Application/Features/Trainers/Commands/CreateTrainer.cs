using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using TrainerManager.Domain.Entities;
using TrainerManager.Domain.ValueObjects;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

namespace TrainerManager.Application.Features.Trainers.Commands
{
    public record CreateTrainerCommand : IRequest<int>
    {
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required"), EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNumber { get; set; } = string.Empty;
        public string? AlternateMobileNumber { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? IdentityNumber { get; set; }

        public string? VisaType { get; set; }
        public string? VisaCountry { get; set; }
        public DateTime? VisaExpiry { get; set; }

        public List<CertificationDto> Certifications { get; set; } = new();

        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;

        public decimal Rate { get; set; }
        public string Currency { get; set; } = "INR";

        public string? BankName { get; set; } = string.Empty;
        public string? AccountNumber { get; set; } = string.Empty;

        public string? TechField { get; set; } = string.Empty;
        public string? Specialization { get; set; } = string.Empty;
        public int Experience { get; set; }
        public string? LastCompanyName { get; set; } = string.Empty;

        public IFormFile? ProfileImage { get; set; }
        public IFormFile? ResumeFile { get; set; }
    }

    public class CertificationDto
    {
        public string? Name { get; set; }
        public string? IssuingOrganization { get; set; }
        public DateTime DateObtained { get; set; }
    }

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
                MobileNumber = request.MobileNumber,
                AlternateMobileNumber = request.AlternateMobileNumber,
                LinkedInUrl = request.LinkedInUrl,
                IdentityNumber = request.IdentityNumber,
                YearsOfExperience = request.Experience,
                Field = request.TechField,
                Specialization = request.Specialization,
                LastCompanyName = request.LastCompanyName,

                // CORRECTED MAPPING
                Visa = new VisaDetails
                {
                    VisaType = request.VisaType,
                    Country = request.VisaCountry,
                    ExpiryDate = request.VisaExpiry, // Fixed Type Mismatch
                    IsWorkAuthorized = !string.IsNullOrEmpty(request.VisaType) // Fixed Type Mismatch
                },

                Address = new Address(
                    request.Street ?? "N/A",
                    request.City ?? "N/A",
                    request.State ?? "N/A",
                    request.Zip ?? "N/A",
                    request.Country ?? "N/A"
                ),

                Costing = new TrainerCosting(request.Rate, request.Currency ?? "INR"),

                AccountDetails = new TrainerAccount
                {
                    BankName = request.BankName ?? "N/A",
                    AccountNumber = request.AccountNumber ?? "N/A"
                },

                ProfileImagePath = request.ProfileImage != null
                    ? await files.SaveFileAsync(request.ProfileImage, "images")
                    : null,
                ResumePath = request.ResumeFile != null
                    ? await files.SaveFileAsync(request.ResumeFile, "resumes")
                    : null
            };

            if (request.Certifications != null)
            {
                foreach (var cert in request.Certifications)
                {
                    trainer.Certifications.Add(new TrainerCertification
                    {
                        Name = cert.Name,
                        IssuingOrganization = cert.IssuingOrganization,
                        DateObtained = cert.DateObtained
                    });
                }
            }

            context.Trainers.Add(trainer);
            await context.SaveChangesAsync(ct);
            return trainer.Id;
        }
    }
}