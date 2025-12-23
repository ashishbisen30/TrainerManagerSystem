using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Domain.ValueObjects
{
    public class TrainerCertification
    {
        public int Id { get; set; } // Add this line
        public string? Name { get; set; } = string.Empty;
        public string? IssuingOrganization { get; set; } = string.Empty;
        public DateTime DateObtained { get; set; }
        public string? CertificateUrl { get; set; }
    }
}
