using Health.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Services
{
    public interface IUsersRepository
    {
        Task<IList<User>> GetUsers();

        Task<User> GetUserById();

        Task<int> AddUserAsync(User user);

        void DeleteUser(User user);
        User GetUserByNameAndPassword(string name, string password);
    }
}
