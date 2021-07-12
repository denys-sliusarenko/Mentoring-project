using Mentoring_project.Business.Interfaces;
using Mentoring_project.Domain.Core.Entities;
using Mentoring_project.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring_project.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CreateUser(User user)
        {
            _unitOfWork.UserRepository.Create(user);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteUser(int id)
        {
            _unitOfWork.UserRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public User GetUserById(int id)
        {
            return _unitOfWork.UserRepository.Get(id);
        }

        public async Task UpdateUser(User user)
        {
            _unitOfWork.UserRepository.Update(user);
            await _unitOfWork.SaveAsync();
        }
    }
}
