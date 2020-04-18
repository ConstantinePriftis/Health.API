using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Dtos
{
    public class FeverDto
    {
        public double Temperature { get; set; }
        public DateTime DateMeasured { get; set; }
    }
}
