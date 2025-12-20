using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Infrastructure.Data;

using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TrainerManager.Infrastructure.Data;

namespace TrainerManager.Application.Features.Trainers.Queries
{
    // We return the DTO, not the Entity
    public record GetTrainerByIdQuery(int Id) : IRequest<TrainerSummaryDto>;

    public class GetTrainerByIdHandler(TrainerDbContext context, IMapper mapper)
        : IRequestHandler<GetTrainerByIdQuery, TrainerSummaryDto>
    {
        public async Task<TrainerSummaryDto> Handle(GetTrainerByIdQuery request, CancellationToken ct)
        {
            // Use ProjectTo to fetch only the columns required by TrainerSummaryDto
            var trainer = await context.Trainers
                .AsNoTracking()
                .Where(t => t.Id == request.Id)
                .ProjectTo<TrainerSummaryDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);

            if (trainer == null)
            {
                throw new KeyNotFoundException($"Trainer with ID {request.Id} was not found.");
            }

            return trainer;
        }
    }
}
