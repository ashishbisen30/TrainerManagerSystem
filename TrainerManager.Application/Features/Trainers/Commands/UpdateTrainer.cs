using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Domain.Entities;
using TrainerManager.Domain.ValueObjects;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

namespace TrainerManager.Application.Features.Trainers.Commands
{
    public record UpdateTrainerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string? LinkedInUrl { get; set; }
        public string? IdentityNumber { get; set; }
        // Visa
        public string? VisaType { get; set; }
        public string? VisaCountry { get; set; }
        public DateTime? VisaExpiry { get; set; }

        // Professional - Use these names consistently
        public string? TechField { get; set; }
        public string? Specialization { get; set; }
        public int Experience { get; set; }
        public string? LastCompanyName { get; set; }
        // Location
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        // Financials
        public decimal Rate { get; set; }
        public string Currency { get; set; } = "INR";
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }
        // Certifications
        public List<CertificationDto> Certifications { get; set; } = new();
        // Media
        public IFormFile? NewProfileImage { get; set; }
        public IFormFile? NewResumeFile { get; set; }
    }

    public class UpdateTrainerHandler(
    TrainerDbContext context,
    IFileStorageService files)
    : IRequestHandler<UpdateTrainerCommand, bool>
    {
        public async Task<bool> Handle(UpdateTrainerCommand request, CancellationToken ct)
        {
            var trainer = await context.Trainers
                .Include(t => t.Certifications)
                .Include(t => t.Visa)
                .FirstOrDefaultAsync(t => t.Id == request.Id, ct);

            if (trainer == null) return false;

            // 🔹 BASIC INFO
            trainer.FirstName = request.FirstName;
            trainer.LastName = request.LastName;
            trainer.Email = request.Email;
            trainer.MobileNumber = request.MobileNumber;
            trainer.LinkedInUrl = request.LinkedInUrl;
            trainer.IdentityNumber = request.IdentityNumber;

            // 🔹 PROFESSIONAL
            trainer.Field = request.TechField;
            trainer.Specialization = request.Specialization;
            trainer.YearsOfExperience = request.Experience;
            trainer.LastCompanyName = request.LastCompanyName;

            // 🔹 ADDRESS
            trainer.Address = new Address(
                request.Street,
                request.City,
                request.State,
                request.Zip,
                request.Country
            );

            // 🔹 VISA
            trainer.Visa ??= new VisaDetails();
            trainer.Visa.VisaType = request.VisaType;
            trainer.Visa.Country = request.VisaCountry;
            trainer.Visa.ExpiryDate = request.VisaExpiry;
            trainer.Visa.IsWorkAuthorized = !string.IsNullOrEmpty(request.VisaType);

            // 🔹 FINANCIAL
            trainer.Costing = new TrainerCosting(request.Rate, request.Currency);
            trainer.AccountDetails = new TrainerAccount
            {
                BankName = request.BankName,
                AccountNumber = request.AccountNumber
            };

            // 🔹 CERTIFICATIONS (CRITICAL FIX)
            trainer.Certifications.Clear();
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

            // 🔹 FILE UPLOADS
            if (request.NewProfileImage != null)
                trainer.ProfileImagePath = await files.SaveFileAsync(request.NewProfileImage, "images");

            if (request.NewResumeFile != null)
                trainer.ResumePath = await files.SaveFileAsync(request.NewResumeFile, "resumes");

            await context.SaveChangesAsync(ct);
            return true;
        }
    }


    /*
    public class UpdateTrainerHandler(TrainerDbContext context, IFileStorageService files)
    : IRequestHandler<UpdateTrainerCommand, bool>
    {
        public async Task<bool> Handle(UpdateTrainerCommand request, CancellationToken ct)
        {
            var trainer = await context.Trainers.FindAsync([request.Id], ct);
            if (trainer == null) return false;

            trainer.FirstName = request.FirstName;
            trainer.LastName = request.LastName;
            trainer.Email = request.Email;
            trainer.Field = request.TechField;
            trainer.Specialization = request.Specialization;
            trainer.YearsOfExperience = request.Experience;
            trainer.LastCompanyName = request.LastCompanyName;

            // Update Value Objects
            trainer.Address = new Address(request.Street, request.City, request.State, request.Zip, request.Country);
            trainer.Costing = new TrainerCosting(request.Rate, request.Currency);
            trainer.AccountDetails = new TrainerAccount { BankName = request.BankName, AccountNumber = request.AccountNumber };

            // Handle new files only if uploaded
            if (request.NewProfileImage != null)
                trainer.ProfileImagePath = await files.SaveFileAsync(request.NewProfileImage, "images");

            if (request.NewResumeFile != null)
                trainer.ResumePath = await files.SaveFileAsync(request.NewResumeFile, "resumes");

            await context.SaveChangesAsync(ct);
            return true;
        }
    }
    */
}
