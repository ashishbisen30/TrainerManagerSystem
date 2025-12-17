using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Domain.ValueObjects;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

namespace TrainerManager.Application.Features.Trainers.Commands
{
    public class UpdateTrainerCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public decimal Rate { get; set; }
        public IFormFile? NewProfileImage { get; set; } // Optional
    }

    public class UpdateTrainerHandler(TrainerDbContext context, IFileStorageService files)
        : IRequestHandler<UpdateTrainerCommand, bool>
    {
        public async Task<bool> Handle(UpdateTrainerCommand request, CancellationToken ct)
        {
            var trainer = await context.Trainers.FirstOrDefaultAsync(t => t.Id == request.Id, ct);
            if (trainer == null) return false;

            trainer.FirstName = request.FirstName;
            trainer.LastName = request.LastName;
            trainer.Email = request.Email;
            trainer.Address = new Address("", request.City, "", "", request.Country);
            trainer.Costing = new TrainerCosting(request.Rate, trainer.Costing.Currency);

            if (request.NewProfileImage != null)
            {
                files.DeleteFile(trainer.ProfileImagePath);
                trainer.ProfileImagePath = await files.SaveFileAsync(request.NewProfileImage, "images");
            }

            await context.SaveChangesAsync(ct);
            return true;
        }
    }
}
