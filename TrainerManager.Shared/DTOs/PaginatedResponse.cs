using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Shared.DTOs
{
    public class PaginatedResponse
    {
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<TrainerSummaryDto> Items { get; set; } = new();
    }
}
