using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

using MediatR;
using Microsoft.AspNetCore.Http;
using TrainerManager.Domain.Entities;
using TrainerManager.Domain.ValueObjects;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

namespace TrainerManager.Application.Features.Trainers.Commands
{
    public record CreateTrainerCommand : IRequest<int>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public decimal Rate { get; set; }
        public string? Currency { get; set; }
        public int Experience { get; set; }
        public string? TechField { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public IFormFile? ResumeFile { get; set; }
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
                Address = new Address("", request.City, "", "", request.Country),
                Costing = new TrainerCosting(request.Rate, request.Currency),
                YearsOfExperience = request.Experience,
                Field = request.TechField,
                ProfileImagePath = await files.SaveFileAsync(request.ProfileImage, "images"),
                ResumePath = await files.SaveFileAsync(request.ResumeFile, "resumes")
            };
            context.Trainers.Add(trainer);
            await context.SaveChangesAsync(ct);
            return trainer.Id;
        }
    }
}
