using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;

namespace MentoringProject.Application.Services
{
    public class CarService : ICarService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CarService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CarDTO> CreateAsync(CarDTO carDto)
        {
            var newCar = _mapper.Map<Car>(carDto);
            await _unitOfWork.CarRepository.Create(newCar);
            await _unitOfWork.SaveAsync();
            var createdCar = _mapper.Map<CarDTO>(newCar);
            return createdCar;
        }

        public async Task DeleteAsync(Guid id)
        {
            var car = await _unitOfWork.CarRepository.GetAsync(id);
            if (car == null)
            {
                throw new NotFoundException($"Car with {id} not found");
            }

            await _unitOfWork.CarRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<CarDTO> GetAll()
        {
            var cars = _unitOfWork.CarRepository.GetAll();
            var carsDto = _mapper.Map<IEnumerable<CarDTO>>(cars);
            return carsDto;
        }

        public async Task<CarDTO> GetAsync(Guid id)
        {
            var car = await _unitOfWork.CarRepository.GetAsync(id);
            if (car == null)
            {
                throw new NotFoundException($"Car with {id} not found");
            }

            var carDto = _mapper.Map<CarDTO>(car);
            return carDto;
        }

        public async Task<CarDTO> UpdateAsync(CarDTO carDto)
        {
            if (!await _unitOfWork.CarRepository.Exist(c => c.Id.Equals(carDto.Id)))
            {
                throw new NotFoundException();
            }

            var car = _mapper.Map<Car>(carDto);
            _unitOfWork.CarRepository.Update(car);
            await _unitOfWork.SaveAsync();
            var updatedCar = _mapper.Map<CarDTO>(car);
            return updatedCar;
        }
    }
}
