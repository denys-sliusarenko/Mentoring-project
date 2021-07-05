using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mentoring_project.Repositories
{
    public interface IUnitOfWork
    {
        UserRepository UserRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
