using AutoMapper;
using MentoringProject.Application.Interfaces;
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
    }
}