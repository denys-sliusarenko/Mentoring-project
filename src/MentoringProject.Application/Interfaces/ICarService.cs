using MentoringProject.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.Interfaces
{
    public interface ICarService
    {
        IEnumerable<CarDTO> GetAll();

        Task<CarDTO> GetAsync(Guid id);

        Task<CarDTO> CreateAsync(CarDTO user);

        Task DeleteAsync(Guid id);

        Task<CarDTO> UpdateAsync(CarDTO user);
    }
}
