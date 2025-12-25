using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class AddressDto
    {
        public string? Street { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? State { get; set; } = string.Empty;
        public string? Zip { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
    }
}
