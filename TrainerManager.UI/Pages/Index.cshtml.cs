using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainerManager.Application.Features.Trainers.Queries; // <--- MUST MATCH STEP 1
using TrainerManager.Application.Features.Trainers.DTOs;

namespace TrainerManager.UI.Pages
{
    public class IndexModel(IMediator mediator) : PageModel
    {
        public DashboardStatsDto Stats { get; set; } = new();

        public async Task OnGetAsync()
        {
            // This should no longer be red/error once Step 1 is built
            Stats = await mediator.Send(new GetDashboardStatsQuery());
        }
    }
}