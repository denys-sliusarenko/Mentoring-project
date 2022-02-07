using System.Collections.Generic;
using System.Threading.Tasks;
using MentoringProject.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Interfaces
{
    public interface IOwnerService
    {
        IEnumerable<OwnerDTO> GetAll();

        Task<OwnerDTO> GetAsync(int id);

        Task<OwnerDTO> CreateAsync(OwnerDTO user);

        Task DeleteAsync(int id);

        Task<OwnerDTO> UpdateAsync(OwnerDTO user);
    }
}
