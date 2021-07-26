using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Repositories;
using System.Threading.Tasks;

namespace MentoringProject.Infrastructure.Data
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
