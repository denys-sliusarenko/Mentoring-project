using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Infrastructure.Data
{
    public class OwnerCarRepository : IRepository<OwnerCar>
    {
        private DbProjectContext _db;

        public OwnerCarRepository(DbProjectContext context)
        {
            this._db = context;
        }

        public async Task Create(OwnerCar item)
        {
            await _db.OwnerCar.AddAsync(item);
        }

        public async Task Delete(params Guid []keys )
        {
            var ownerCar = await _db.OwnerCar.FindAsync(keys);
            if (ownerCar != null)
            {
                _db.OwnerCar.Remove(ownerCar);
            }
        }

        public async Task<bool> Exist(Expression<Func<OwnerCar, bool>> predicate)
        {
            return await _db.OwnerCar.AnyAsync(predicate);
        }

        public IEnumerable<OwnerCar> GetAll()
        {
            return _db.OwnerCar;
        }

        public async Task<OwnerCar> GetAsync(params Guid[] keys)
        {
            var ownerCar = await _db.OwnerCar.FindAsync(keys);
            return ownerCar;
        }

        public void Update(OwnerCar item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
