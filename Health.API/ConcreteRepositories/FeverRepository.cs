using Health.API.Contexts;
using Health.API.Models;
using Health.API.RepositoryServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Repositories
{
    public class FeverRepository : IFeverRepository
    {
        private HealthContext _healthContext;

        public FeverRepository(HealthContext healthContext)
        {
            _healthContext = healthContext;
        }

        public void AddFever(Fever fever)
        {
            _healthContext.Fevers.Add(fever);
        }

        public void DeleteFever(Fever fever)
        {
            _healthContext.Fevers.Remove(fever);
        }

        public IEnumerable<Fever> GetFeversForUSer(Guid userId)
        {
            return _healthContext.Fevers.Where(x => x.UserId == userId).ToList();
        }

        public bool Save()
        {
            return (_healthContext.SaveChanges() > 1);
        }
    }
}
