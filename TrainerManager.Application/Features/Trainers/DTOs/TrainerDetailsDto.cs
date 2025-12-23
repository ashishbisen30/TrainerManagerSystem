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

        // --- FIXED: Use DTO types here, NOT Domain types ---
        public AddressDto Address { get; set; } = new();
        public CostingDto Costing { get; set; } = new();
        public AccountDto? AccountDetails { get; set; }

        // --- FIXED: Flatten Visa properties so the UI finds 'VisaType' ---
        public string? VisaType { get; set; }
        public string? VisaCountry { get; set; }
        public DateTime? VisaExpiry { get; set; }

        public List<CertificationDto> Certifications { get; set; } = new();
        public string? ProfileImagePath { get; set; }
        public string? ResumePath { get; set; }
        public string? Field { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string? LastCompanyName { get; set; }
    }
}
