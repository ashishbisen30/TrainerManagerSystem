using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Domain.ValueObjects
{
    //[Owned] // <--- Add this
    public class VisaDetails
    {
        public int Id { get; set; } // Add this line
        public string? VisaType { get; set; }
        public string? Country { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsWorkAuthorized { get; set; }
    }
}
