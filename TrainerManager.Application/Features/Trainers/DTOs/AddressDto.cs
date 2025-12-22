using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Application.Features.Trainers.DTOs
{
    public class AddressDto
    {
        public string Street { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string Zip { get; set; } = "";
        public string Country { get; set; } = "";
    }
}
