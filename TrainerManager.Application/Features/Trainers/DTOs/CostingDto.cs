using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class CostingDto
    {
        public decimal HourlyRate { get; set; }
        public string Currency { get; set; } = "INR";
    }
}
