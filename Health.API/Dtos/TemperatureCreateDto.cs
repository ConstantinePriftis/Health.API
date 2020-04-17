using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Dtos
{
    public class TemperatureCreateDto
    {
        //[Required]
        //public Guid UserId { get; set; }
        [Required]
        public double TemperatureValue { get; set; }
    }
}
