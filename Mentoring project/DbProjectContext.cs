using Mentoring_project.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentoring_project
{
    public class DbProjectContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbProjectContext(DbContextOptions<DbProjectContext> options)
            : base(options)
        {
        }
    }
}
