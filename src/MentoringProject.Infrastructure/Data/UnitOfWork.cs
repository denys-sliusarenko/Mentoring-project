using System.Threading.Tasks;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;

namespace MentoringProject.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbProjectContext _db;
        private IRepository<Owner> _ownerRepository;
        private IRepository<Car> _carRepository;

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

        public IRepository<Car> CarRepository
        {
            get
            {
                if (_carRepository == null)
                {
                    _carRepository = new CarRepository(_db);
                }

                return _carRepository;
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
