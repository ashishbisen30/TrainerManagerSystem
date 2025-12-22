using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class TrainerDetailsDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Field { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string? LastCompanyName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? ResumePath { get; set; }

        // You must include these objects if the HTML calls @Model.Trainer.Address...
        public AddressDto Address { get; set; } = new();
        public CostingDto Costing { get; set; } = new();
        public AccountDto AccountDetails { get; set; } = new();

        // Helper property often used in Details headers
        public string FullName => $"{FirstName} {LastName}";
    }
}
