using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Infrastructure.Data
{
    internal class OwnerCarRepository : IRepository<OwnerCar>
    {
        private DbProjectContext _db;

        public OwnerCarRepository(DbProjectContext context)
        {
            this._db = context;
        }

        public async Task Create(OwnerCar item)
        {
            await _db.OwnerCars.AddAsync(item);
        }

        public async Task Delete(params object[]keys)
        {
            var ownerCar = await _db.OwnerCars.FindAsync(keys);
            if (ownerCar != null)
            {
                _db.OwnerCars.Remove(ownerCar);
            }
        }

        public async Task<bool> Exist(Expression<Func<OwnerCar, bool>> predicate)
        {
            return await _db.OwnerCars.AnyAsync(predicate);
        }

        public IEnumerable<OwnerCar> GetAll()
        {
            return _db.OwnerCars;
        }

        public async Task<OwnerCar> GetAsync(params object[] keys)
        {
            var ownerCar = await _db.OwnerCars.FindAsync(keys);
            return ownerCar;
        }

        public void Update(OwnerCar item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
