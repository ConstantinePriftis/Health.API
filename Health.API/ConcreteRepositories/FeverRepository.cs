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
            throw new NotImplementedException();
        }

        public void DeleteFever(Guid feverId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fever> GetFeversForUSer(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
