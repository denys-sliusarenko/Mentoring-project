using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentoring_project.Entities
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new DbProjectContext(serviceProvider.GetRequiredService<DbContextOptions<DbProjectContext>>()))
            {
                if (context.Users.Any())
                {
                    return;
                }

                context.Users.AddRange(
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
                         UserId =3,
                         FirstName = "Alice",
                         LastName = "Wolker"
                     }
                  );

                context.SaveChanges();
            }
        }
    }
}