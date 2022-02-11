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


        //public async Task<OwnerCarDTO> CreateAsync(OwnerCarDTO ownerCarDto)
        //{
        //    var newOwnerCar = _mapper.Map<OwnerCar>(ownerCarDto);
        //    await _unitOfWork.OwnerCarRepository.Create(newOwnerCar);
        //    await _unitOfWork.SaveAsync();
        //    var createdOwnerCar = _mapper.Map<OwnerCarDTO>(newOwnerCar);
        //    return createdOwnerCar;
        //}

        //public async Task DeleteAsync(Guid id)
        //{
        //    var ownerCar = await _unitOfWork.OwnerRepository.GetAsync(id);
        //    if (ownerCar == null)
        //    {
        //        throw new NotFoundException($"Owner car with {id} not found");
        //    }

        //    await _unitOfWork.OwnerCarRepository.Delete(id);
        //    await _unitOfWork.SaveAsync();
        //}

        //public IEnumerable<OwnerCarDTO> GetAll()
        //{
        //    var ownersCars = _unitOfWork.OwnerCarRepository.GetAll();
        //    var ownersCarsDto = _mapper.Map<IEnumerable<OwnerCarDTO>>(ownersCars);
        //    return ownersCarsDto;
        //}

        //public async Task<OwnerCarDTO> GetAsync(Guid id)
        //{
        //    var ownerCar = await _unitOfWork.OwnerCarRepository.GetAsync(id);
        //    if (ownerCar == null)
        //    {
        //        throw new NotFoundException($"Owner car with {id} not found");
        //    }

        //    var ownerDto = _mapper.Map<OwnerCarDTO>(ownerCar);

        //    return ownerDto;
        //}

        //public async Task<OwnerCarDTO> UpdateAsync(OwnerCarDTO ownerCarDto)
        //{
        //    if (!await _unitOfWork.OwnerCarRepository.Exist(o=> o.CarId.Equals( ownerCarDto.CarId)&&o.OwnerId.Equals(ownerCarDto.OwnerId)))
        //    {
        //        throw new NotFoundException();
        //    }

        //    var ownerCar = _mapper.Map<OwnerCar>(ownerCarDto);
        //    _unitOfWork.OwnerCarRepository.Update(ownerCar);
        //    await _unitOfWork.SaveAsync();
        //    var updatedOwnerCar = _mapper.Map<OwnerCarDTO>(ownerCar);
        //    return updatedOwnerCar;
        //}
    }
}
