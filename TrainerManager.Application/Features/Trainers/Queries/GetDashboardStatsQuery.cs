using MediatR;
using Microsoft.EntityFrameworkCore;
using TrainerManager.Application.Features.Trainers.DTOs;
using TrainerManager.Infrastructure.Data;

namespace TrainerManager.Application.Features.Trainers.Queries
{
    public record GetDashboardStatsQuery : IRequest<DashboardStatsDto>;

    public class GetDashboardStatsQueryHandler : IRequestHandler<GetDashboardStatsQuery, DashboardStatsDto>
    {
        private readonly TrainerDbContext _context;

        public GetDashboardStatsQueryHandler(TrainerDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsDto> Handle(GetDashboardStatsQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.Today;
            var thirtyDaysFromNow = today.AddDays(30);

            var stats = new DashboardStatsDto();

            // 1. Total Trainers Count
            stats.TotalTrainers = await _context.Trainers.CountAsync(cancellationToken);

            // 2. Total Certifications (Count across all trainers)
            // Note: Since Certifications is an "Owned Many" collection, we use SelectMany to count them
            stats.TotalCertifications = await _context.Trainers
                .SelectMany(t => t.Certifications)
                .CountAsync(cancellationToken);

            // 3. Expiring Visas (Corrected to map to your Visa.ExpiryDate property)
            stats.ExpiringVisasCount = await _context.Trainers
                .Where(t => t.Visa.ExpiryDate >= today && t.Visa.ExpiryDate <= thirtyDaysFromNow)
                .CountAsync(cancellationToken);

            // 4. Average Hourly Rate (Mapped to your Costing value object)
            var rates = await _context.Trainers
                .Select(t => (decimal?)t.Costing.HourlyRate) // Cast to nullable decimal to handle empty lists
                .ToListAsync(cancellationToken);

            stats.AverageHourlyRate = rates.Any() ? rates.Average() ?? 0 : 0;

            // 5. Recent Trainers (Top 5)
            stats.RecentTrainers = await _context.Trainers
                .OrderByDescending(t => t.Id)
                .Take(5)
                .Select(t => new RecentTrainerDto
                {
                    Id = t.Id,
                    FullName = (t.FirstName ?? "") + " " + (t.LastName ?? ""),
                    Field = t.Field ?? "N/A",
                    Experience = t.YearsOfExperience
                })
                .ToListAsync(cancellationToken);

            return stats;
        }
    }
}