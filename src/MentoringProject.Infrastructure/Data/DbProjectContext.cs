using MentoringProject.Domain.Entities;
using MentoringProject.Infrastructure.ConfigurationModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentoringProject.Infrastructure.Data
{
    public class DbProjectContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }

        public DbSet<Car> Cars { get; set; }

        public DbSet<OwnerCar> OwnerCars { get; set; }

        public DbProjectContext(DbContextOptions<DbProjectContext> options)
            : base(options)
        {
            // Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerCarConfiguration());
            modelBuilder.Entity<Owner>().HasData(
                new Owner[]
                {
                    new Owner()
                    {
                        FirstName = "Tom",
                        LastName = "Wolker",
                    },
                    new Owner()
                    {
                         FirstName = "Adam",
                         LastName = "Wolker",
                    },
                    new Owner()
                    {
                        FirstName = "Alice",
                        LastName = "Wolker",
                    },
                });

            modelBuilder.Entity<Car>().HasData(
               new Car[]
               {
                    new Car()
                    {
                        Brand = "BMW",
                        Color = "Red",
                    },
                    new Car()
                    {
                        Brand = "Mercedes",
                        Color = "Black",
                    },
                    new Car()
                    {
                        Brand = "Nissan",
                        Color = "White",
                    },
               });
        }
    }
}
