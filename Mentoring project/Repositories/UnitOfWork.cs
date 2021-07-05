using Mentoring_project.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentoring_project.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProjectContext _db;
        private UserRepository userRepository;

        public UnitOfWork(DbProjectContext db)
        {
            _db = db;
        }

        public UserRepository UserRepository
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
