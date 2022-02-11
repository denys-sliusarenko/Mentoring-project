using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Infrastructure.Data
{
    internal class OwnerRepository : IRepository<Owner>
    {
        private DbProjectContext _db;

        public OwnerRepository(DbProjectContext context)
        {
            this._db = context;
        }

        public async Task Create(Owner item)
        {
            await _db.Owners.AddAsync(item);
        }

        public async Task Delete(Guid id)
        {
            Owner user = await _db.Owners.FindAsync(id);
            if (user != null)
            {
                _db.Owners.Remove(user);
            }
        }

        public async Task<bool> Exist(Expression<Func<Owner, bool>> predicate)
        {
            return await _db.Owners.AnyAsync(predicate);
        }

        public async Task<Owner> GetAsync(Guid id)
        {
            var owner = await _db.Owners.FindAsync(id);
            return owner;
        }

        public IEnumerable<Owner> GetAll()
        {
            return _db.Owners;
        }

        public void Update(Owner item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
