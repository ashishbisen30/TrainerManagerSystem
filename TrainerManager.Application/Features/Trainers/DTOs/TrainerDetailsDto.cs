using System;
using System.Collections.Generic;
using System.Text;
using TrainerManager.Application.Features.Trainers.Commands;
using TrainerManager.Domain.ValueObjects;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class TrainerDetailsDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        // ADD THESE - The Details Page is throwing errors because these are missing
        public string MobileNumber { get; set; } = string.Empty;
        public string? LinkedInUrl { get; set; }
        public string? IdentityNumber { get; set; }
        public VisaDetails? Visa { get; set; } // Should match your Domain object
        public List<CertificationDto> Certifications { get; set; } = new();

        public string? ProfileImagePath { get; set; }
        public string? ResumePath { get; set; }
        public string? Field { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string? LastCompanyName { get; set; }
        public Address? Address { get; set; }
        public TrainerCosting? Costing { get; set; }
    }
}
