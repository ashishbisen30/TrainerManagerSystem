using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Domain.Entities;
using TrainerManager.Domain.ValueObjects;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class TrainerDetailsDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string MobileNumber { get; set; } = string.Empty;
        public string? LinkedInUrl { get; set; }
        public string? IdentityNumber { get; set; }

        // Grouped Objects - UI will use @Model.Trainer.Address.Street
        public AddressDto Address { get; set; } = new();
        public CostingDto Costing { get; set; } = new();
        public AccountDto AccountDetails { get; set; } = new();

        // Visa remains flat for easy UI binding
        public string? VisaType { get; set; }
        public string? VisaCountry { get; set; }
        public DateTime? VisaExpiry { get; set; }

        public string? Field { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string? LastCompanyName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? ResumePath { get; set; }

        public List<CertificationDto> Certifications { get; set; } = new();
    }
}
