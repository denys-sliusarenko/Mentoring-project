using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Infrastructure.Data
{
    class CarRepository : IRepository<Car>
    {
        private DbProjectContext _db;

        public CarRepository(DbProjectContext context)
        {
            this._db = context;
        }

        public async Task Create(Car item)
        {
            await _db.Cars.AddAsync(item);
        }

        public async Task Delete(params Guid[] keys)
        {
            var car = await _db.Cars.FindAsync(keys);
            if (car != null)
            {
                _db.Cars.Remove(car);
            }
        }

        public async Task<bool> Exist(Expression<Func<Car, bool>> predicate/*Guid id*/)
        {
            return await _db.Cars.AnyAsync(predicate); //d => d.Id == id
        }

        public async Task<Car> GetAsync(params Guid[] keys)
        {
            var car = await _db.Cars.FindAsync(keys);
            return car;
        }

        public IEnumerable<Car> GetAll()
        {
            return _db.Cars;
        }

        public void Update(Car item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }
    }
}
