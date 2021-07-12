using Mentoring_project.Domain.Core.Entities;
using Mentoring_project.Infrastructure.Data.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MentoringProject.Tests
{
    public class DbFixture
    {
        public DbContextOptions<DbProjectContext> GetDbOptions()
        {
            string dbName = Guid.NewGuid().ToString();

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
                new User { UserId=1, FirstName="Tom",LastName="Walker"},
                new User { UserId=2, FirstName="Alice", LastName="Walker"},
                new User { UserId=3, FirstName="Sam",LastName="Walker"}
            };
            return users;
        }

    }
}
