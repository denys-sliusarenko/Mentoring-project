﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Services;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Controllers
{
    [Route("api/owners")]
    [ApiController]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerService _ownerService;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerService ownerService, IMapper mapper)
        {
            _ownerService = ownerService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ownersDto = _ownerService.GetAll();
            var owners = _mapper.Map<ICollection<OwnerViewModel>>(ownersDto);

            using (var log = new Logger(Encoding.UTF8))
            {
                log.WriteLog("Get all owners");
            }

            return Ok(owners);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var ownerDto = await _ownerService.GetAsync(id);
            var owner = _mapper.Map<OwnerViewModel>(ownerDto);

            return Ok(owner);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOwnerViewModel owner)
        {
            var newOwnerDto = _mapper.Map<OwnerDTO>(owner);
            var createdOwner = await _ownerService.CreateAsync(newOwnerDto);
            return CreatedAtAction(nameof(Get), new { id = createdOwner.Id }, createdOwner);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            await _ownerService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOwnerViewModel owner)
        {
            var newOwnerDto = _mapper.Map<OwnerDTO>(owner);
            var updatedOwner = await _ownerService.UpdateAsync(newOwnerDto);
            return Ok(updatedOwner);
        }
    }
}