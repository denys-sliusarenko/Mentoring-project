using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using System.Threading.Tasks;

namespace MentoringProject.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProjectContext _db;
        private IRepository<Owner> _ownerRepository;

        public UnitOfWork(DbProjectContext db)
        {
            _db = db;
        }

        public IRepository<Owner> OwnerRepository
        {
            get
            {
                if (_ownerRepository == null)
                {
                    _ownerRepository = new OwnerRepository(_db);
                }

                return _ownerRepository;
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
