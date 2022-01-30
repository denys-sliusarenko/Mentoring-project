using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Services;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Infrastructure.Data;
using MentoringProject.TestDataConfiguration;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MentoringProject.Web.Tests
{
    public class UserServiceTest : IClassFixture<DbFixture>
    {
        private readonly IMapper _mapper;
        private readonly DbProjectContext _context;
        private readonly UserService _userService;

        public UserServiceTest(DbFixture dbFixture)
        {
            _mapper = MapperConfig.GetMapper();
            _context = new DbProjectContext(dbFixture.GetDbOptions());
            var uow = new UnitOfWork(_context);
            _userService = new UserService(uow, _mapper);
        }

        [Fact]
        public void GetUserById_WhenExists_ReturnCorrectUser()
        {
            // Arrange
            var expectedUser = _mapper.Map<UserDTO>(_context.Users.First());

            // Act
            var result = _userService.GetUserById(expectedUser.UserId);

            // Assert
            result.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void GetUserById_WhenNotExists_ThrowUserException()
        {
            // Arrange
            int idUser = int.MinValue;

            // Act
            Action act = () => _userService.GetUserById(idUser);

            // Assert
            Assert.Throws<NotFoundException>(act);
        }

        [Fact]
        public void GetAll_WhenGetAllUserFromDatabase_ReturnAllUsers()
        {
            // Arrange
            var allUsers = _context.Users;

            // Act
            var result = _userService.GetAll();

            // Assert
            result.Should().BeEquivalentTo(allUsers);
        }

        [Fact]
        public async Task CreateUserAsync_CreateCorrectUser_ReturnCreatedUser()
        {
            // Arrange
            var uow = new UnitOfWork(_context);
            var userService = new UserService(uow, _mapper);
            var newUser = new UserDTO()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
            };

            // Act
            var createdUser = await userService.CreateUserAsync(newUser);
            var allUsers = _mapper.Map<IEnumerable<UserDTO>>(_context.Users);

            // Assert
            allUsers.Should().ContainEquivalentOf(createdUser);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenExistUser_RemovedUser()
        {
            // Arrange
            int idUser = _context.Users.First().UserId;

            // Act
            await _userService.DeleteUserAsync(idUser);
            var deletedUser = _context.Users.Find(idUser);

            // Assert
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task DeleteUserAsyn_WhenNotExistUser_ThrowsUserException()
        {
            // Arrange
            int idDeleteUser = int.MinValue;
            int expectedCount = _context.Users.Count();
            var allUsers = _context.Users;

            // Act
            Func<Task> act = () => _userService.DeleteUserAsync(idDeleteUser);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExist_ReturnUpdatedUser()
        {
            // Act
            var user = _mapper.Map<UserDTO>(_context.Users.AsNoTracking().First());

            user.FirstName = Guid.NewGuid().ToString();
            user.LastName = Guid.NewGuid().ToString();

            var updatedUser = await _userService.UpdateUserAsync(user);
            var savedUser = _mapper.Map<UserDTO>(await _context.Users.FindAsync(updatedUser.UserId));

            // Assert
            updatedUser.Should().BeEquivalentTo(savedUser);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var user = new UserDTO()
            {
                UserId = int.MinValue,
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
            };

            // Act
            Func<Task> act = () => _userService.UpdateUserAsync(user);

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}