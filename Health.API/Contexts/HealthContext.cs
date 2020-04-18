using Health.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Health.API.Contexts
{
    public class HealthContext : DbContext
    {
        public HealthContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=Health;user=root;password=admin");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Temperature> Temperatures { get; set; }
        public DbSet<Fever> Fevers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<User>(user =>
            {
                user.HasKey(k => k.Id);
                user.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
                user.Property(p => p.LastName).IsRequired().HasMaxLength(100);
                user.Property(p => p.Password).IsRequired();
                user.Property(p => p.Age);

                user.HasMany<Temperature>()
                .WithOne()
                .HasForeignKey(fk => fk.UserId);

                user.HasMany<Fever>()
                .WithOne()
                .HasForeignKey(fk => fk.UserId);
            });


            modelbuilder.Entity<Temperature>(temp =>
            {
                temp.HasKey(k => k.Id);
                temp.Property(p => p.TemperatureValue).IsRequired();
                temp.Property(p => p.DateMeasured).IsRequired();
                temp.Property(p => p.HasFever);
            });
        }

    }
}
