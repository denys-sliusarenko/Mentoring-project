using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MentoringProject.Application.DTO;

namespace MentoringProject.Application.Interfaces
{
    public interface IOwnerCarService
    {
        IEnumerable<OwnerCarDTO> GetOwnerCarsAsync(Guid idOwner);

        Task<OwnerCarDTO> CreateOwnerCarAsync(OwnerCarDTO ownerCarDto);

        Task DeleteAsync(Guid idOwner);
    }
}
