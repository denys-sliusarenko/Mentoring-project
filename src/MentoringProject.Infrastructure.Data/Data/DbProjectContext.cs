using MentoringProject.Domain.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Infrastructure.Data.Data
{
    public class DbProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbProjectContext(DbContextOptions<DbProjectContext> options)
            : base(options)
        {
           // Database.EnsureDeleted();
           // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User[]
                {
                    new User()
                    {
                        UserId = 1,
                        FirstName = "Tom",
                        LastName = "Wolker"
                    },
                    new User()
                    {
                         UserId = 2,
                         FirstName = "Adam",
                         LastName = "Wolker"
                    },
                    new User()
                    {
                        UserId = 3,
                        FirstName = "Alice",
                        LastName = "Wolker"
                    }
                });
        }
    }
}
