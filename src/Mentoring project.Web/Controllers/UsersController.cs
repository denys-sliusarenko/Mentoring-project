﻿using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Services;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            using (var log = new Logger(Encoding.UTF8))
            {
                log.WriteLog("Get all users");
            }

            return Ok(users);
        }

        //[HttpGet]
        //[Route("getFile")]
        //public IActionResult GetFile()
        //{
        //    var file = _userService.GetFile();
        //    return file;
        //}

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
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
            await _userService.DeleteUserAsync(id);
            return NoContent();
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