using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Services;
using MentoringProject.Domain.Core.Entities;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Infrastructure.Data;
using MentoringProject.TestDataConfiguration;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MentoringProject.Web.Tests
{
    public class UserServiceTest : IClassFixture<TestDataFixture>
    {
        private readonly IMapper _mapper;
        private readonly TestDataFixture _testDataFixture;

        public UserServiceTest(TestDataFixture testDataFixture)
        {
            _mapper = MapperConfig.GetMapper();
            _testDataFixture = testDataFixture;
        }

        [Fact]
        public void GetUserById_WhenExists_ReturnCorrectUser()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.Get(It.IsAny<int>())).Returns(_testDataFixture.GetTestUsers().First());
            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            var result = userService.GetUserById(It.IsAny<int>());

            // Assert
            unitOfWorkMock.Verify(u => u.UserRepository.Get(It.IsAny<int>()));
        }

        [Fact]
        public void GetUserById_WhenNotExists_ThrowUserException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.Get(It.IsAny<int>())).Throws(new NotFoundException());
            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            Action act = () => userService.GetUserById(It.IsAny<int>());

            // Assert
            Assert.Throws<NotFoundException>(act);
        }

        [Fact]
        public void GetAll_WhenGetAllUserFromDatabase_ReturnAllUsers()
        {
            // Arrange
            var allUsers = _testDataFixture.GetTestUsers();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.GetAll()).Returns(allUsers);
            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            var result = userService.GetAll();

            // Assert
            unitOfWorkMock.Verify(u => u.UserRepository.GetAll());

            result.Should().BeEquivalentTo(allUsers);
        }

        [Fact]
        public async Task CreateUserAsync_CreateCorrectUser_ReturnCreatedUser()
        {
            // Arrange

            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.Create(It.IsAny<User>()));

            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            var createdUser = await userService.CreateUserAsync(It.IsAny<UserDTO>());

            // Assert
            unitOfWorkMock.Verify(u => u.UserRepository.Create(It.IsAny<User>()));
        }

        [Fact]
        public async Task DeleteUserAsync_WhenExistUser_RemovedUser()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.Get(It.IsAny<int>())).Returns(_testDataFixture.GetTestUsers().First());
            unitOfWorkMock.Setup(u => u.UserRepository.Delete(It.IsAny<int>()));

            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            await userService.DeleteUserAsync(It.IsAny<int>());

            // Assert
            unitOfWorkMock.Verify(u => u.UserRepository.Delete(It.IsAny<int>()));
        }

        [Fact]
        public async Task DeleteUserAsync_WhenNotExistUser_ThrowsUserException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.Get(It.IsAny<int>())).Returns((User)null);

            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            Func<Task> act = () => userService.DeleteUserAsync(It.IsAny<int>());

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExist_ReturnUpdatedUser()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.Exist(It.IsAny<int>())).ReturnsAsync(true);
            unitOfWorkMock.Setup(u => u.UserRepository.Update(It.IsAny<User>())).Verifiable();

            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            var updatedUser = await userService.UpdateUserAsync(_testDataFixture.GetTestDTOUsers().First());

            // Assert
            unitOfWorkMock.Verify(u => u.UserRepository.Update(It.IsAny<User>()));
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.UserRepository.Exist(It.IsAny<int>())).ReturnsAsync(false);

            var userService = new UserService(unitOfWorkMock.Object, _mapper);

            // Act
            Func<Task> act = () => userService.UpdateUserAsync(_testDataFixture.GetTestDTOUsers().First());

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(act);
        }
    }
}