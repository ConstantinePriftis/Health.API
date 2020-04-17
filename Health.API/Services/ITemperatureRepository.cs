using Health.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Services
{
    public interface ITemperatureRepository
    {
        void AddTemperatureForUser(Temperature temperature);

        IEnumerable<Temperature> GetTemperaturesByUserId(Guid userId);

        Temperature GetTemperatureById(Guid id);

    }
}
