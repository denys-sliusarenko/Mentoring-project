using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Domain.Core.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : class
    {
        IEnumerable<T> GetAll();

        Task<T> GetAsync(params Guid[] keys);

        Task Create(T item);

        void Update(T item);

        Task Delete(params Guid[] keys);

        Task<bool> Exist(Expression<Func<T, bool>> predicate);
    }
}
