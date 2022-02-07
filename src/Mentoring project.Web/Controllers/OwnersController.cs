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
            var owners = _ownerService.GetAll();
            using (var log = new Logger(Encoding.UTF8))
            {
                log.WriteLog("Get all owners");
            }

            return Ok(owners);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var owner = await _ownerService.GetAsync(id);
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
        public async Task<IActionResult> DeleteAsync(int id)
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