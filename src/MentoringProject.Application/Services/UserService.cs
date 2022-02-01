using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace MentoringProject.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(UserDTO userDto)
        {
            var newUser = _mapper.Map<User>(userDto);
            _unitOfWork.UserRepository.Create(newUser);
            await _unitOfWork.SaveAsync();
            var createdUser = _mapper.Map<UserDTO>(newUser);
            return createdUser;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = _unitOfWork.UserRepository.Get(id);
            if (user == null)
            {
                throw new NotFoundException($"User with {id} not found");
            }

            _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<UserDTO> GetAll()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            var usersDto = _mapper.Map<IEnumerable<UserDTO>>(users);
            return usersDto;
        }

        public FileStreamResult GetFile()
        {
            var users = _unitOfWork.UserRepository.GetAll();
            MemoryStream ms = new ();
            StreamWriter sw = new (ms);
            foreach (var t in users)
            {
                sw.WriteLine($"{t.UserId} {t.FirstName} {t.LastName}");
            }

            sw.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            FileStreamResult result = new (ms, MediaTypeNames.Text.Plain)
            {
                FileDownloadName = "test.txt",
            };

            return result;
        }

        public UserDTO GetUserById(int id)
        {
            var user = _unitOfWork.UserRepository.Get(id);
            if (user == null)
            {
                throw new NotFoundException($"User with {id} not found");
            }

            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDto)
        {
            if (!await _unitOfWork.UserRepository.Exist(userDto.UserId))
            {
                throw new NotFoundException();
            }

            var user = _mapper.Map<User>(userDto);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            var updatedUser = _mapper.Map<UserDTO>(user);
            return updatedUser;
        }
    }
}