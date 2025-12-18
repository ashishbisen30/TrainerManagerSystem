using MediatR;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions; // Necessary for ProjectTo
using TrainerManager.Infrastructure.Data;
using TrainerManager.Application.Features.Trainers.DTOs;

namespace TrainerManager.Application.Features.Trainers.Queries
{
    public record PaginatedResponse(int TotalCount, int PageIndex, int PageSize, IEnumerable<TrainerSummaryDto> Items);

    public record GetTrainersQuery : IRequest<PaginatedResponse>
    {
        public int PageIndex { get; init; } = 1;
        public int PageSize { get; init; } = 10;
        public string? Search { get; init; }
        public string? OrderBy { get; init; }
        public bool IsDescending { get; init; } = false;
    }

    public class GetTrainersHandler(TrainerDbContext context, IMapper mapper)
    : IRequestHandler<GetTrainersQuery, PaginatedResponse>
    {
        public async Task<PaginatedResponse> Handle(GetTrainersQuery request, CancellationToken ct)
        {
            var query = context.Trainers.AsNoTracking();

            // 1. IMPROVED SEARCH: Multi-field and Related Tables
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                var search = request.Search.Trim();
                query = query.Where(t =>
                    t.FirstName.Contains(search) ||
                    t.LastName.Contains(search) ||
                    t.Email.Contains(search) ||
                    t.Field.Contains(search) ||
                    // Searching inside the related TrainingHistory table
                    t.TrainingHistory.Any(h => h.TechnologyTaught.Contains(search)));
            }

            // 2. EXPANDED SORTING: Added Rate and Experience
            query = request.OrderBy?.ToLower() switch
            {
                "firstname" => request.IsDescending ? query.OrderByDescending(t => t.FirstName) : query.OrderBy(t => t.FirstName),
                "lastname" => request.IsDescending ? query.OrderByDescending(t => t.LastName) : query.OrderBy(t => t.LastName),
                "experience" => request.IsDescending ? query.OrderByDescending(t => t.YearsOfExperience) : query.OrderBy(t => t.YearsOfExperience),
                "rate" => request.IsDescending ? query.OrderByDescending(t => t.Costing.HourlyRate) : query.OrderBy(t => t.Costing.HourlyRate),
                _ => query.OrderBy(t => t.Id) // Always have a stable default sort
            };

            // 3. EXECUTION
            var totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<TrainerSummaryDto>(mapper.ConfigurationProvider)
                .ToListAsync(ct);

            return new PaginatedResponse(totalCount, request.PageIndex, request.PageSize, items);
        }
    }
}