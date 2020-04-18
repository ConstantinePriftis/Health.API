using Health.API.Contexts;
using Health.API.Models;
using Health.API.ResourceParameters;
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
        }

        public void DeleteTemperature(Temperature temperature)
        {
            _healthContext.Remove(temperature);
        }

        public Temperature GetTemperatureById(Guid id, Guid userId)
        {
            Temperature temp = null;
            try
            {
                temp = _healthContext.Temperatures.FirstOrDefault(x => x.Id == id && x.UserId == userId);
                return temp;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public IEnumerable<Temperature> GetTemperaturesByUserId(Guid userId, ResourceParameters.ResourceParams resourceParams)
        {
            try
            {
                var temperaturesCollection = _healthContext.Temperatures as IQueryable<Temperature>;
                var collectionToReturn = temperaturesCollection.Where(t => t.UserId == userId);
                return collectionToReturn
                         .Skip(resourceParams.PageSize * (resourceParams.PageNumber - 1))
                         .Take(resourceParams.PageSize)
                         .ToList();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public bool Save()
        {
            return (_healthContext.SaveChanges() > 1);
        }
    }
}
