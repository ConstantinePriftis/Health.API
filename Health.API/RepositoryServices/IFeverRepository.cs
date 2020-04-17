using Health.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.RepositoryServices
{
    interface IFeverRepository
    {
        IEnumerable<Fever> GetFeversForUSer(Guid userId);

        void AddFever(Fever fever);

        void DeleteFever(Guid feverId);
    }
}
