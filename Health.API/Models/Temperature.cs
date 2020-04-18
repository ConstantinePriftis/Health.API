using System;
using System.Collections.Generic;
using System.Text;

namespace Health.API.Models
{
    public class Temperature
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public double TemperatureValue { get; set; }
        public DateTime DateMeasured { get; set; }
        public DateTime DateModified { get; set; }
        public bool HasFever { get; set; }

        private Temperature(Guid userId, double temperatureValue, bool hasFever = false)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId == Guid.Empty ? throw new ArgumentNullException(nameof(userId)) : userId;
            this.TemperatureValue = temperatureValue;
            DateMeasured = DateTime.UtcNow;
            HasFever = hasFever;
        }

        public static Temperature Create(Guid userId, double temperature, bool hasFever = false)
        {
            return new Temperature(userId, temperature, hasFever);
        }

        public void ModifyTemperature(double temperature, bool hasFever)
        {
            this.TemperatureValue = temperature;
            this.HasFever = hasFever;
            DateModified = DateTime.UtcNow;
        }

    }
}
