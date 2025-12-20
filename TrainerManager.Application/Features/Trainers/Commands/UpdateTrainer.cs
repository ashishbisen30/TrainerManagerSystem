using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Domain.ValueObjects;
using TrainerManager.Infrastructure.Data;
using TrainerManager.Infrastructure.Services;

namespace TrainerManager.Application.Features.Trainers.Commands
{
    // Returns a DTO of the updated trainer to the frontend
    public record UpdateTrainerCommand : IRequest<TrainerSummaryDto>
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Field { get; set; } = string.Empty;
        public int YearsOfExperience { get; set; }
        public decimal HourlyRate { get; set; } // For the Costing nested object
    }

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
}
