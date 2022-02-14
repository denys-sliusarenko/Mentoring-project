using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Controllers
{
    [Route("api/ownercar")]
    [ApiController]
    public class OwnerCarController : ControllerBase
    {
        private readonly IOwnerCarService _ownerCarService;
        private readonly IMapper _mapper;

        public OwnerCarController(IOwnerCarService ownerCarService, IMapper mapper)
        {
            _ownerCarService = ownerCarService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getOwnerCars/{idOwner}")]
        public IActionResult Get(Guid idOwner)
        {
            var ownerCarsDto = _ownerCarService.GetOwnerCarsAsync(idOwner);
            var ownerCars = _mapper.Map<IEnumerable<OwnerCarViewModel>>(ownerCarsDto);
            return Ok(ownerCars);
        }

        [HttpPost]
        [Route("addCarOwner")]
        public async Task<IActionResult> AddCarToOwner(OwnerCarCreateViewModel createOwnerCarViewModel)
        {
            var ownerCarsDto = _mapper.Map<OwnerCarDTO>(createOwnerCarViewModel);
            var newOwnerCarDto = await _ownerCarService.CreateOwnerCarAsync(ownerCarsDto);
            var newOwnerCarViewModel = _mapper.Map<OwnerCarViewModel>(newOwnerCarDto);
            return Ok(newOwnerCarViewModel);
        }

        [HttpDelete]
        [Route("{idOwnerCar}")]
        public async Task<IActionResult> DeleteCarInOwner(Guid idOwnerCar)
        {
            await _ownerCarService.DeleteAsync(idOwnerCar);
            return NoContent();
        }
    }
}