using Health.API.Contexts;
using Health.API.Models;
using Health.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Repositories
{
    public class TemperatureRepository : ITemperatureRepository
    {
        private HealthContext _healthContext;

        public TemperatureRepository(HealthContext healthContext)
        {
            _healthContext = healthContext;
        }
        public void AddTemperatureForUser(Temperature temperature)
        {
            if (temperature == null)
                throw new ArgumentNullException(nameof(temperature));
            _healthContext.Temperatures.Add(temperature);
            _healthContext.SaveChanges();

        }

        public Temperature GetTemperatureById(Guid id)
        {
            Temperature temp = null;
            try
            {
                temp = _healthContext.Temperatures.FirstOrDefault(x => x.Id == id);
                return temp;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<Temperature> GetTemperaturesByUserId(Guid userId)
        {
            IEnumerable<Temperature> listOfTemperatures = new List<Temperature>();
            try
            {
                listOfTemperatures = _healthContext.Temperatures.Where(x => x.UserId == userId).ToList();
                return listOfTemperatures;
            }
            catch (Exception)
            {
                return null;
            }

        }
    }
}
