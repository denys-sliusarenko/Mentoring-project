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

        public DbSet<OwnerCar> OwnerCar { get; set; }

        public DbProjectContext(DbContextOptions<DbProjectContext> options)
            : base(options)
        {
            // Database.EnsureDeleted();
            // Database.EnsureCreated();
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
        //    modelBuilder.Entity<OwnerCar>().Property(x => x.RegistrationNumber).HasValueGenerator();
            //(DatabaseGeneratedOption.Identity)
            // modelBuilder.Entity<Car>().HasMany(c => c.Owners)
            //     .WithMany(s => s.Cars)
            //     .UsingEntity<UserCar>(
            //  j => j
            //     .HasOne(pt => pt.Owner)
            //     .WithMany(t => t.UserCars)
            //     .HasForeignKey(pt => pt.OwnerId),
            //  j => j
            //    .HasOne(pt => pt.Car)
            //    .WithMany(p => p.UserCars)
            //    .HasForeignKey(pt => pt.CarId),
            //  j =>
            //  {
            //      j.Property(pt => pt.TestColumn).HasDefaultValueSql("TestColumn");
            //      j.HasKey(t => new { t.CarId, t.OwnerId });
            //      j.ToTable("UserCar");
            //});

            modelBuilder.Entity<Owner>().HasData(
                new Owner[]
                {
                    new Owner()
                    {
                        //Id = 1,
                        FirstName = "Tom",
                        LastName = "Wolker",
                    },
                    new Owner()
                    {
                       //  Id = 2,
                         FirstName = "Adam",
                         LastName = "Wolker",
                    },
                    new Owner()
                    {
                       // Id = 3,
                        FirstName = "Alice",
                        LastName = "Wolker",
                    },
                });

            modelBuilder.Entity<Car>().HasData(
               new Car[]
               {
                    new Car()
                    {
                       //Id = 1,
                        Brand = "BMW",
                        Color = "Red",
                    },
                    new Car()
                    {
                        // Id = 2,
                        Brand = "Mercedes",
                        Color = "Black",
                    },
                    new Car()
                    {
                        //Id = 3,
                        Brand  = "Nissan",
                        Color = "White",
                    },
               });
        }
    }
}
