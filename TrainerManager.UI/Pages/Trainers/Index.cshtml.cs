using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TrainerManager.Application.Features.Trainers.Queries;
using TrainerManager.Application.Features.Trainers.DTOs; // Points to the CLASS version

namespace TrainerManager.UI.Pages.Trainers
{
    public class IndexModel : PageModel
    {
        private readonly IMediator _mediator;

        public IndexModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        // This works because the PaginatedResponse CLASS in DTOs has an empty constructor
        public PaginatedResponse Data { get; set; } = new();

        public async Task OnGetAsync(string? search, int pageIndex = 1)
        {
            var query = new GetTrainersQuery
            {
                Search = search,
                PageIndex = pageIndex,
                PageSize = 10
            };

            Data = await _mediator.Send(query);
        }
    }
}