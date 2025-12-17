using System;
using System.Collections.Generic;
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
        public ICollection<ClientTrainingHistory> TrainingHistory { get; set; } = new List<ClientTrainingHistory>();
    }
}
