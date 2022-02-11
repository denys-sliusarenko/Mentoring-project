using MentoringProject.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.Interfaces
{
    public interface IOwnerCarService
    {
        //  IEnumerable<OwnerCarDTO> GetAll();

        IEnumerable<OwnerCarDTO> GetOwnerCarsAsync(Guid idOwner);

      //  Task<OwnerCarDTO> CreateAsync(OwnerCarDTO ownerCarDto);

       // Task DeleteAsync(Guid id);

       // Task<OwnerCarDTO> UpdateAsync(OwnerCarDTO ownerCarDto);
    }
}
