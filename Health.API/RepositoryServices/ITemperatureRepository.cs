using Health.API.Models;
using Health.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Services
{
    public interface ITemperatureRepository
    {
        void AddTemperatureForUser(Temperature temperature);

        IEnumerable<Temperature> GetTemperaturesByUserId(Guid userId, ResourceParams resourceParams);

        Temperature GetTemperatureById(Guid id, Guid userId);

        void DeleteTemperature(Temperature temperature);

        bool Save();
    }
}
