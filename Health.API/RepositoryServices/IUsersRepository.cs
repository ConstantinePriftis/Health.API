using Health.API.Models;
using Health.API.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Services
{
    public interface IUsersRepository
    {
        IEnumerable<User> GetUsers(ResourceParams resourceParams);

        User GetUserById(Guid id);

        void AddUser(User user);

        void DeleteUser(User user);

        bool Save();

        User GetUserByNameAndPassword(string name, string password);


    }
}
