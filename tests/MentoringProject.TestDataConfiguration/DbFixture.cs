using MentoringProject.Domain.Core.Entities;
using MentoringProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MentoringProject.TestDataConfiguration
{
    public class DbFixture
    {
        public DbContextOptions<DbProjectContext> GetDbOptions()
        {
            string dbName = "DbMentoringProject";

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<DbProjectContext>();
            builder.UseInMemoryDatabase(dbName)
                   .UseInternalServiceProvider(serviceProvider);
            using (var context = new DbProjectContext(builder.Options))
            {
                context.Users.AddRange(GetTestUsers());
                context.SaveChanges();
            }
            return builder.Options;
        }

        private List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    FirstName="Tom",
                    LastName="Walker"
                },
                new User
                {
                    FirstName="Alice",
                    LastName="Walker"
                },
                new User
                {
                    FirstName="Sam",
                    LastName="Walker"
                }
            };
            return users;
        }
    }
}
