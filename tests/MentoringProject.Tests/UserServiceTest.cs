using AutoMapper;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Exceptions;
using MentoringProject.Application.Services;
using MentoringProject.Infrastructure.Data;
using MentoringProject.Mapper;
using MentoringProject.TestDataConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace MentoringProject.Web.Tests
{
    public class UserServiceTest : IClassFixture<DbFixture>
    {
        private DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public UserServiceTest(DbFixture dbFixture)
        {
            _dbFixture = dbFixture;
            _mapper = MapperConfig.GetMapper();
        }

        [Fact]
        public void GetUserById_WhenExists_ReturnCorrectUser()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var expectedUser = _mapper.Map<UserDTO>(context.Users.First());

                // Act
                var result = userService.GetUserById(expectedUser.UserId);

                //Assert
                result.Should().BeEquivalentTo(expectedUser);
            }
        }

        [Fact]
        public void GetUserById_WhenNotExists_ThrowUserException()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int IdUser = int.MinValue;

                //Act
                Action act = () => userService.GetUserById(IdUser);

                //Assert

                var exception = Assert.Throws<UserException>(act);

                Assert.Equal($"User with {IdUser} not found", exception.Message);
            }
        }

        [Fact]
        public void GetAll_WhenGetAllUserFromDatabase_ReturnAllUsers()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var allUsers = context.Users;

                // Act
                var result = userService.GetAll();

                //Assert
                result.Should().BeEquivalentTo(allUsers);
            }
        }

        [Fact]
        public async Task CreateUserAsync_CreateCorrectUser_ReturnCreatedUser()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var newUser = new UserDTO()
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };

                //Act
                var createdUser = await userService.CreateUserAsync(newUser);
                var allUsers = _mapper.Map<IEnumerable<UserDTO>>(context.Users);

                //Assert
                allUsers.Should().ContainEquivalentOf(createdUser);
            }
        }

        [Fact]
        public async Task DeleteUserAsync_WhenExistUser_RemovedUser()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int idUser = context.Users.First().UserId;

                //Act
                await userService.DeleteUserAsync(idUser);
                var deletedUser = context.Users.Find(idUser);

                //Assert
                Assert.Null(deletedUser);
            }
        }

        [Fact]
        public async Task DeleteUserAsyn_WhenNotExistUser_ThrowsUserException()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int idDeleteUser = int.MinValue;
                int expectedCount = context.Users.Count();
                var allUsers = context.Users;

                // Act
                Func<Task> act = () => userService.DeleteUserAsync(idDeleteUser);

                //Assert

                var exception = await Assert.ThrowsAsync<UserException>(act);
                Assert.Equal($"User with {idDeleteUser} not found", exception.Message);
            }
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExist_ReturnUpdatedUser()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);

                // Act
                var user = _mapper.Map<UserDTO>(context.Users.AsNoTracking().First());

                user.FirstName = Guid.NewGuid().ToString();
                user.LastName = Guid.NewGuid().ToString();

                var updatedUser = await userService.UpdateUserAsync(user);
                var savedUser = _mapper.Map<UserDTO>(await context.Users.FindAsync(updatedUser.UserId));

                //Assert
                updatedUser.Should().BeEquivalentTo(savedUser);
            }
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserNotExist_ThrowsDbUpdateConcurrencyException()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var user = new UserDTO()
                {
                    UserId = int.MinValue,
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };

                // Act
                Func<Task> act = () => userService.UpdateUserAsync(user);

                //Assert
                await Assert.ThrowsAsync<DbUpdateConcurrencyException>(act);
            }
        }
    }
}