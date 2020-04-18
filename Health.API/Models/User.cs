using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Health.API.Models
{
    public class User : IdentityUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public User()
        {

        }

        public User(string firstName, string lastName, string password, int age)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Password = password;
            this.Age = age;
        }

        public void ModifyUser(
            string firstName,
            string password,
            string lastName,
            int age)
        {
            this.FirstName = firstName;
            this.Password = password;
            this.LastName = lastName;
            this.Age = age;
        }
    }
}
