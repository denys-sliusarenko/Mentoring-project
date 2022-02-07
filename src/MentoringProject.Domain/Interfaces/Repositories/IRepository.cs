using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> GetAsync(Guid id);

        Task Create(T item);

        void Update(T item);

        Task Delete(Guid id);

        Task<bool> Exist(Guid id);
    }
}
