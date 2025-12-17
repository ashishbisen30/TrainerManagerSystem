using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerManager.Domain.ValueObjects
{
    public record Address(string Street, string City, string State, string Zip, string Country);
}
