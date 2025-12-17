using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Domain.ValueObjects
{
    public record TrainerCosting(decimal HourlyRate, string Currency);
}
