using MentoringProject.Domain.Core.Entities;
using MentoringProject.Infrastructure.ConfigurationModel;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Infrastructure.Data
{
    public class DbProjectContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }

        public DbProjectContext(DbContextOptions<DbProjectContext> options)
            : base(options)
        {
          //  Database.EnsureDeleted();
           Database.EnsureCreated();
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new OwnerConfiguration());

            modelBuilder.Entity<Owner>().HasData(
                new Owner[]
                {
                    new Owner()
                    {
                        Id = 1,
                        FirstName = "Tom",
                        LastName = "Wolker",
                    },
                    new Owner()
                    {
                         Id = 2,
                         FirstName = "Adam",
                         LastName = "Wolker",
                    },
                    new Owner()
                    {
                        Id = 3,
                        FirstName = "Alice",
                        LastName = "Wolker",
                    },
                });
        }
    }
}
