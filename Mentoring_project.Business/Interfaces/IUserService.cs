using Mentoring_project.Business.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentoring_project.Business.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAll();
        UserDTO GetUserById(int id);
        Task CreateUserAsync(UserDTO user);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(UserDTO user);
    }
}
