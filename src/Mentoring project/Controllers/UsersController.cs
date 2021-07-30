using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var user = _userService.GetUserById(id);
                return Ok(user);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserViewModel user)
        {
            var newUserDto = _mapper.Map<UserDTO>(user);
            var createdUser = await _userService.CreateUserAsync(newUserDto);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUserAsync(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserViewModel user)
        {
            try
            {
                var newUserDto = _mapper.Map<UserDTO>(user);
                var updatedUser = await _userService.UpdateUserAsync(newUserDto);
                return Ok(updatedUser);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex);
            }
        }
    }
}
