using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MentoringProject.Application.DTO;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Interfaces
{
    public interface IOwnerService
    {
        IEnumerable<OwnerDTO> GetAll();

        Task<OwnerDTO> GetAsync(Guid id);

        Task<OwnerDTO> CreateAsync(OwnerDTO ownerDto);

        Task DeleteAsync(Guid id);

        Task<OwnerDTO> UpdateAsync(OwnerDTO ownerDto);
    }
}
