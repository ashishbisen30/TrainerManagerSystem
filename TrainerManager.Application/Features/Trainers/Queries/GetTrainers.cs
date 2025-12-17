using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Infrastructure.Data;

namespace TrainerManager.Application.Features.Trainers.Queries
{
    public record GetTrainersQuery(int Page = 1, string Tech = null, string Loc = null) : IRequest<object>;

    public class GetTrainersHandler(TrainerDbContext context) : IRequestHandler<GetTrainersQuery, object>
    {
        public async Task<object> Handle(GetTrainersQuery request, CancellationToken ct)
        {
            var query = context.Trainers.AsNoTracking();
            if (!string.IsNullOrEmpty(request.Tech))
                query = query.Where(t => t.Field.Contains(request.Tech) || t.TrainingHistory.Any(h => h.TechnologyTaught.Contains(request.Tech)));
            if (!string.IsNullOrEmpty(request.Loc))
                query = query.Where(t => t.Address.City.Contains(request.Loc));

            var items = await query.Skip((request.Page - 1) * 10).Take(10).ToListAsync(ct);
            return new { Total = await query.CountAsync(ct), Items = items };
        }
    }
}
