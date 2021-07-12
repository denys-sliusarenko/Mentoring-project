using Mentoring_project.Domain.Core.Entities;
using Mentoring_project.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring_project.Infrastructure.Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProjectContext _db;
        private IRepository<User> userRepository;

        public UnitOfWork(DbProjectContext db)
        {
            _db = db;
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_db);
                return userRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }

}
