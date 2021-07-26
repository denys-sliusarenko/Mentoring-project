using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MentoringProject.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserViewModel user)
        {
            var newUserDto = _mapper.Map<UserDTO>(user);
            var createdUser = await _userService.CreateUserAsync(newUserDto);

            return Created($"{Request.Path.Value}/{createdUser.UserId}", createdUser);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            var user = _userService.GetUserById(id);
            if (user != null)
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserViewModel user)
        {
            var newUserDto = _mapper.Map<UserDTO>(user);
            var updatedUser = await _userService.UpdateUserAsync(newUserDto);
            return Ok(updatedUser);
        }
    }
}
