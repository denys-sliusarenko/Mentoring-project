using MentoringProject.Domain.Core.Entities;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Core.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }

        void Save();

        Task SaveAsync();
    }
}
