using Mentoring_project.Domain.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring_project.Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetUserById(int id);
        Task CreateUser(User user);
        Task DeleteUser(int id);
        Task UpdateUser(User user);
    }
}
