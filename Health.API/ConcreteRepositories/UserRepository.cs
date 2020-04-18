using Health.API.Contexts;
using Health.API.Models;
using Health.API.ResourceParameters;
using Health.API.Services;
using Microsoft.EntityFrameworkCore;
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

        public void AddUser(User user)
        {
            _healthContext.Users.Add(user ?? throw new ArgumentNullException(nameof(user)));
        }

        public void DeleteUser(User user)
        {
            _healthContext.Users.Remove(user ?? throw new ArgumentNullException(nameof(user)));
        }

        public User GetUserById(Guid id)
        {
            return _healthContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetUserByNameAndPassword(string name, string password)
        {
            try
            {
                var user = _healthContext.Users.FirstOrDefault(x => x.FirstName == name && x.Password == password);
                return user;
            }
            catch (Exception e)
            {

                return null;
            }
        }

        public IEnumerable<User> GetUsers(ResourceParams resourceParams)
        {
            try
            {
                var userCollection = _healthContext.Users as IQueryable<User>;
                return userCollection
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
