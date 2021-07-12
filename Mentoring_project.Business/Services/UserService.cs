using AutoMapper;
using Mentoring_project.Business.DTO;
using Mentoring_project.Business.Interfaces;
using Mentoring_project.Domain.Core.Entities;
using Mentoring_project.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mentoring_project.Business.Services
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

        public async Task CreateUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
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
            var userDto = _mapper.Map<UserDTO>(user);

            return userDto;
        }

        public async Task UpdateUserAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }
    }
}
