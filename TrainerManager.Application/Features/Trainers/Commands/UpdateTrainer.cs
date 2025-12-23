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
    // Returns a DTO of the updated trainer to the frontend
    //public record UpdateTrainerCommand : IRequest<TrainerSummaryDto>
    //{
    //    public int Id { get; set; }
    //    public string FirstName { get; set; } = string.Empty;
    //    public string LastName { get; set; } = string.Empty;
    //    public string Email { get; set; } = string.Empty;
    //    public string PhoneNumber { get; set; } = string.Empty;
    //    public string Field { get; set; } = string.Empty;
    //    public int YearsOfExperience { get; set; }
    //    public decimal HourlyRate { get; set; } // For the Costing nested object
    //}
    public record UpdateTrainerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Field { get; set; }
        public string? Specialization { get; set; }
        public int Experience { get; set; }
        public string? LastCompanyName { get; set; }

        // Address
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip { get; set; } = "";
        public string Country { get; set; } = "";

        // Finance
        public decimal Rate { get; set; }
        public string Currency { get; set; } = "INR";
        public string? BankName { get; set; }
        public string? AccountNumber { get; set; }

        // Files (Optional for updates)
        public IFormFile? NewProfileImage { get; set; }
        public IFormFile? NewResumeFile { get; set; }
    }

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
            trainer.Field = request.Field;
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
    /*
    public class UpdateTrainerHandler(TrainerDbContext context, IMapper mapper)
        : IRequestHandler<UpdateTrainerCommand, TrainerSummaryDto>
    {
        public async Task<TrainerSummaryDto> Handle(UpdateTrainerCommand request, CancellationToken ct)
        {
            // 1. Fetch the existing entity (do NOT use AsNoTracking because we want to update it)
            var trainer = await context.Trainers
                .Include(t => t.Costing) // Include owned entities or relations if necessary
                .FirstOrDefaultAsync(t => t.Id == request.Id, ct);

            if (trainer == null)
            {
                // You can throw a custom NotFoundException here
                throw new KeyNotFoundException($"Trainer with ID {request.Id} was not found.");
            }

            // 2. Map the request values onto the existing trainer object
            // AutoMapper updates 'trainer' directly with 'request' data
            mapper.Map(request, trainer);

            // 3. Save changes
            await context.SaveChangesAsync(ct);

            // 4. Return the updated version mapped to a DTO
            return mapper.Map<TrainerSummaryDto>(trainer);
        }
    }
    */
}
