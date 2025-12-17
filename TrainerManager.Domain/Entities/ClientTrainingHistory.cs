using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Domain.Entities
{
    public class ClientTrainingHistory
    {
        public int Id { get; set; }
        public int TrainerId { get; set; }
        public string? ClientCompanyName { get; set; }
        public string? TechnologyTaught { get; set; }
        public int DurationInDays { get; set; }
        public DateTime StartDate { get; set; }
    }
}
