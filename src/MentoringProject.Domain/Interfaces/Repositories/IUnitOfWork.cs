using System.Threading.Tasks;
using MentoringProject.Domain.Entities;

namespace MentoringProject.Domain.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<Owner> OwnerRepository { get; }

        IRepository<Car> CarRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
