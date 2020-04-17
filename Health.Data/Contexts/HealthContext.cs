using Health.Domain.DomainEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Health.Data.Contexts
{
    public class HealthContext : DbContext
    {
        public HealthContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);
            modelbuilder.Entity<Temperature>(temp =>
            {
                temp.HasKey(k => k.Id);
                temp.Property(p => p.TemperatureValue).IsRequired();
                temp.Property(p => p.DateMeasured).IsRequired();

                temp.HasOne<User>()
                .WithMany().HasForeignKey(fk => fk.UserId);
            });

            modelbuilder.Entity<Fever>(fever =>
            {
                fever.HasKey(k => k.Id);
                fever.Property(p => p.Temperature).IsRequired();
                fever.Property(p => p.DateMeasured).IsRequired();

                fever.HasOne<User>()
                .WithMany()
                .HasForeignKey(fk => fk.UserId);
            });
        }

    }
}
