using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TrainerManager.Domain.ValueObjects;

namespace TrainerManager.Domain.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Address Address { get; set; }
        public TrainerCosting Costing { get; set; }
        public TrainerAccount AccountDetails { get; set; }
        public string? Field { get; set; }
        public string? Specialization { get; set; }
        public int YearsOfExperience { get; set; }
        public string? LastCompanyName { get; set; }
        public string? ProfileImagePath { get; set; }
        public string? ResumePath { get; set; }
        // ... existing ...
        [Required(ErrorMessage = "Mobile Number is required")]
        public string MobileNumber { get; set; } = string.Empty;
        public string? AlternateMobileNumber { get; set; }
        public string? AlternateEmail { get; set; }
        public string? LinkedInUrl { get; set; }
        public string? IdentityNumber { get; set; }

        public ICollection<ClientTrainingHistory> TrainingHistory { get; set; } = new List<ClientTrainingHistory>();

        public List<TrainerCertification> Certifications { get; set; } = new();
        public VisaDetails Visa { get; set; } = new();
    }
}
