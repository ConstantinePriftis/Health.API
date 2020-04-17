using System;
using System.Collections.Generic;
using System.Text;

namespace Health.Domain.DomainEntities
{
    public class Temperature
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public double TemperatureValue { get; set; }
        public DateTime DateMeasured { get; set; }

        private Temperature(Guid userId, double temperatureValue)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId == Guid.Empty ? throw new ArgumentNullException(nameof(userId)) : userId;
            this.TemperatureValue = temperatureValue;
            DateMeasured = DateTime.UtcNow;
        }

        public static Temperature Create(Guid userId, double temperature)
        {
            return new Temperature(userId, temperature);
        }

    }
}
