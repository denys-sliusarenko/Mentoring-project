using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MentoringProject.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMapper _mapper;

        public CarsController(ICarService carService, IMapper mapper)
        {
            _carService = carService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var carsDto = _carService.GetAll();
            var cars = _mapper.Map<ICollection<CarViewModel>>(carsDto);
            return Ok(cars);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var carDto = await _carService.GetAsync(id);
            var car = _mapper.Map<CarViewModel>(carDto);
            return Ok(car);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CarCreateViewModel owner)
        {
            var newCarDto = _mapper.Map<CarDTO>(owner);
            var createdCar = await _carService.CreateAsync(newCarDto);
            var createdCarViewModel = _mapper.Map<CarViewModel>(createdCar);
            return CreatedAtAction(nameof(Get), new { id = createdCarViewModel.Id }, createdCarViewModel);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _carService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CarUpdateViewModel owner)
        {
            var newCarDto = _mapper.Map<CarDTO>(owner);
            var updatedCar = await _carService.UpdateAsync(newCarDto);
            var updatedCarViewModel = _mapper.Map<OwnerViewModel>(updatedCar);
            return Ok(updatedCarViewModel);
        }
    }
}