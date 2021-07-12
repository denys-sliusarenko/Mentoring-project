using AutoMapper;
using Mentoring_project.Business.DTO;
using Mentoring_project.Business.Interfaces;
using Mentoring_project.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Mentoring_project.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel user)
        {
            try
            {
                var newUserDto = _mapper.Map<UserDTO>(user);
                await _userService.CreateUserAsync(newUserDto);

                return Created($"{Request.Path.Value}/{newUserDto.UserId}", newUserDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserViewModel user)
        {
            try
            {
                var newUserDto = _mapper.Map<UserDTO>(user);
                await _userService.UpdateUserAsync(newUserDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
