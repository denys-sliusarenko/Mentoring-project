using MentoringProject.Application.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentoringProject.Application.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAll();

        UserDTO GetUserById(int id);

        Task<UserDTO> CreateUserAsync(UserDTO user);

        Task DeleteUserAsync(int id);

        Task<UserDTO> UpdateUserAsync(UserDTO user);
    }
}
