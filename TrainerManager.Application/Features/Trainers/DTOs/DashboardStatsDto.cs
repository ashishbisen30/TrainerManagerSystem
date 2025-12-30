using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalTrainers { get; set; }
        public int TotalCertifications { get; set; }
        public int ExpiringVisasCount { get; set; }
        public decimal AverageHourlyRate { get; set; }
        public List<RecentTrainerDto> RecentTrainers { get; set; } = new();
    }
}
