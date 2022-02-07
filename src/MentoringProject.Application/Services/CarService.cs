using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProject.Application.Services
{
    public class CarService : ICarService
    {
        public Task<CarDTO> CreateAsync(CarDTO user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CarDTO> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CarDTO> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CarDTO> UpdateAsync(CarDTO user)
        {
            throw new NotImplementedException();
        }
    }
}
