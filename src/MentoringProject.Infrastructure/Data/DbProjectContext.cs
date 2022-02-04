using MentoringProject.Domain.Core.Entities;
using MentoringProject.Infrastructure.ConfigurationModel;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Infrastructure.Data
{
    public class DbProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbProjectContext(DbContextOptions<DbProjectContext> options)
            : base(options)
        {
          //  Database.EnsureDeleted();
           Database.EnsureCreated();
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new UserConfiguration());

            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User()
                    {
                        Id = 1,
                        FirstName = "Tom",
                        LastName = "Wolker",
                    },
                    new User()
                    {
                         Id = 2,
                         FirstName = "Adam",
                         LastName = "Wolker",
                    },
                    new User()
                    {
                        Id = 3,
                        FirstName = "Alice",
                        LastName = "Wolker",
                    },
                });
        }
    }
}
