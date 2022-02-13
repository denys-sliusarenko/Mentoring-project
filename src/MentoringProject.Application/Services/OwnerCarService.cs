using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;

namespace MentoringProject.Application.Services
{
    public class OwnerCarService : IOwnerCarService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerCarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<OwnerCarDTO> GetOwnerCarsAsync(Guid idOwner)
        {
            var ownerCars = _unitOfWork.OwnerCarRepository.GetAll().Where(o => o.OwnerId.Equals(idOwner));
            var ownersCarsDto = _mapper.Map<IEnumerable<OwnerCarDTO>>(ownerCars);
            return ownersCarsDto;
        }

        public async Task<OwnerCarDTO> CreateOwnerCarAsync(OwnerCarDTO ownerCarDto)
        {
            var newOwnerCar = _mapper.Map<OwnerCar>(ownerCarDto);
            await _unitOfWork.OwnerCarRepository.Create(newOwnerCar);
            await _unitOfWork.SaveAsync();
            var createdOwnerCar = _mapper.Map<OwnerCarDTO>(newOwnerCar);
            return createdOwnerCar;
        }

        public async Task DeleteAsync(Guid idOwnerCar)
        {
            var ownerCar = await _unitOfWork.OwnerCarRepository.GetAsync(idOwnerCar);
            if (ownerCar == null)
            {
                throw new NotFoundException($"Owner car with {idOwnerCar} not found");
            }

            await _unitOfWork.OwnerCarRepository.Delete(idOwnerCar);
            await _unitOfWork.SaveAsync();
        }
    }
}
