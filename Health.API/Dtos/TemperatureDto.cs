using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Dtos
{
    public class TemperatureDto
    {
        public Guid UserId { get; set; }
        public double TemperatureValue { get; set; }
        public DateTime DateMeasured { get; set; }
    }
}
