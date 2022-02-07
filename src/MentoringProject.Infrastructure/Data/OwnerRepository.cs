using System.Collections.Generic;
using System.Threading.Tasks;
using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Infrastructure.Data
{
    public class OwnerRepository : IRepository<Owner>
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

        public async Task Delete(int id)
        {
            Owner user = await _db.Owners.FindAsync(id);
            if (user != null)
            {
                _db.Owners.Remove(user);
            }
        }

        public async Task<bool> Exist(int id)
        {
           return await _db.Owners.AnyAsync(d => d.Id == id);
        }

        public async Task<Owner> GetAsync(int id)
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
