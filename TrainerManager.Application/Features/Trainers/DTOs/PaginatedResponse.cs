using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class PaginatedResponse
    {
        // Explicit default constructor fixes the "no argument given" error
        public PaginatedResponse()
        {
            Items = new List<TrainerSummaryDto>();
        }

        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<TrainerSummaryDto> Items { get; set; }
    }
    //{
    //    // 1. Add this empty constructor explicitly
    //    public PaginatedResponse()
    //    {
    //        Items = new List<TrainerSummaryDto>();
    //    }

    //    // 2. Use standard properties with { get; set; }
    //    public int TotalCount { get; set; }
    //    public int PageIndex { get; set; }
    //    public int PageSize { get; set; }
    //    public List<TrainerSummaryDto> Items { get; set; }

    //    //// Adding a parameterless constructor fixes the "No argument given" error
    //    //public PaginatedResponse() { }

    //    //public int TotalCount { get; set; }
    //    //public int PageIndex { get; set; }
    //    //public int PageSize { get; set; }
    //    //public List<TrainerSummaryDto> Items { get; set; } = new();
    //}
}
