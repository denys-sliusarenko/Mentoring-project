using AutoMapper;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Exceptions;
using MentoringProject.Application.Interfaces;
using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                throw new UserException($"User with {id} not found");
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

        public UserDTO GetUserById(int id)
        {
            var user = _unitOfWork.UserRepository.Get(id);
            if (user == null)
            {
                throw new UserException($"User with {id} not found");
            }
            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }

        public async Task<UserDTO> UpdateUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
            var updatedUser = _mapper.Map<UserDTO>(user);
            return updatedUser;
        }
    }
}