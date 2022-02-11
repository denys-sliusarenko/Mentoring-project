using AutoMapper;
using MentoringProject.Application.Interfaces;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        //[HttpGet]
        //public IActionResult Get()
        //{
        //    var ownerCarsDto = _ownerCarService.GetAll();
        //    var cars = _mapper.Map<ICollection<OwnerCarViewModel>>(ownerCarsDto);
        //    return Ok(cars);
        //}

        [HttpGet]
        [Route("{idOwner}")]
        public IActionResult Get(Guid idOwner)
        {
            var ownerCarsDto = _ownerCarService.GetOwnerCarsAsync(idOwner);
            var ownerCars = _mapper.Map<IEnumerable<OwnerCarViewModel>>(ownerCarsDto);
            return Ok(ownerCars);
        }
    }
}