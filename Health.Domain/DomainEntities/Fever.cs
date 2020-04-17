using System;
using System.Collections.Generic;
using System.Text;

namespace Health.Domain.DomainEntities
{
    public class Fever
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public double Temperature { get; set; }
        public DateTime DateMeasured { get; set; }

        private Fever(Guid userId, double temperature)
        {
            this.Id = Guid.NewGuid();
            this.UserId = userId == Guid.Empty ? throw new ArgumentNullException(nameof(userId)) : userId;
            this.Temperature = temperature;
            this.DateMeasured = DateTime.UtcNow;
        }

        public static Fever Create(Guid userId, double temperature)
        {
            return new Fever(userId, temperature);
        }
    }
}
