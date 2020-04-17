using Health.API.Contexts;
using Health.API.Models;
using Health.API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Repositories
{
    public class UserRepository : IUsersRepository
    {
        private HealthContext _healthContext;

        public UserRepository(HealthContext healthContext)
        {
            _healthContext = healthContext;
        }

        public async Task<int> AddUserAsync(User user)
        {
            _healthContext.Users.Add(user ?? throw new ArgumentNullException(nameof(user)));
            int modifiedRows = 0;
            try
            {
                modifiedRows = _healthContext.SaveChanges();
            }
            catch (Exception e)
            {

                return modifiedRows;
            }

            return modifiedRows;
        }

        public async void DeleteUser(User user)
        {

        }

        public Task<User> GetUserById()
        {
            throw new NotImplementedException();
        }

        public User GetUserByNameAndPassword(string name, string password)
        {
            try
            {
                var something = _healthContext.Users.FirstOrDefault(x => x.FirstName == name && x.Password == password);
                return something;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public Task<IList<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
