using Mentoring_project.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring_project.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        //  UserRepository UserRepository { get; }
        void Save();
        Task SaveAsync();
    }
}
