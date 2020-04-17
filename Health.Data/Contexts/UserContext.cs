using Health.Domain.DomainEntities;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Health.Data.Contexts
{
    public class UserContext : IdentityDbContext
    {
        public UserContext(DbContextOptions options) 
        {
        }
    }
}
