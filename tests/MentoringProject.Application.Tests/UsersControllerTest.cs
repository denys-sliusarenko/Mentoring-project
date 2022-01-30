using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Application.Services;
using MentoringProject.Controllers;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Infrastructure.Data;
using MentoringProject.TestDataConfiguration;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace MentoringProject.Web.Tests
{
    public class UsersControllerTest : IClassFixture<TestDataFixture>
    {
        private readonly IMapper _mapper;
        private readonly TestDataFixture _testDataFixture;

        public UsersControllerTest(TestDataFixture testDataFixture)
        {
            _mapper = MapperConfig.GetMapper();
            _testDataFixture = testDataFixture;
        }

        [Fact]
        public void GetUsers_WhenGetAllUserFromDatabase_ReturnAllUsersStatusOk()
        {
            // Arrange
            var expectedUsers = _testDataFixture.GetTestDTOUsers();
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.GetAll()).Returns(expectedUsers);

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = controller.GetUsers() as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            result.Value.Should().BeEquivalentTo(expectedUsers);
        }


        [Fact]
        public void GetUserById_WhenUserExist_ReturnUserStatusOk()
        {
            // Arrange
            var expectedUser = _testDataFixture.GetTestDTOUsers().First();

            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Returns(expectedUser);

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = controller.GetUserById(It.IsAny<int>()) as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            result.Value.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void GetUserById_WhenUserNotExist_ReturnNotFoundStatus()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.GetUserById(It.IsAny<int>())).Throws(new NotFoundException());

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = controller.GetUserById(It.IsAny<int>()) as ObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_CreateCorrectUser_ReturnCreatedUserStatusOk()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.CreateUserAsync(It.IsAny<UserDTO>()))
            .ReturnsAsync(_testDataFixture.GetTestDTOUsers().First());

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = await controller.CreateUserAsync(It.IsAny<CreateUserViewModel>()) as ObjectResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserExist_ReturnStatusNoContent()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.DeleteUserAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = await controller.DeleteUserAsync(It.IsAny<int>()) as StatusCodeResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserNotExist_ReturnStatusNoFound()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.DeleteUserAsync(It.IsAny<int>())).Throws(new NotFoundException());

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = await controller.DeleteUserAsync(It.IsAny<int>()) as ObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExist_ReturnUpdatedUserStatusOk()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.UpdateUserAsync(It.IsAny<UserDTO>())).ReturnsAsync(_testDataFixture.GetTestDTOUsers().First());

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = await controller.UpdateUserAsync(It.IsAny<UpdateUserViewModel>()) as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserNotExist_ReturnNotFoundObjectrResult()
        {
            // Arrange
            var userServiceMock = new Mock<IUserService>();
            userServiceMock.Setup(repo => repo.UpdateUserAsync(It.IsAny<UserDTO>())).Throws(new NotFoundException());

            var controller = new UsersController(userServiceMock.Object, _mapper);

            // Act
            var result = await controller.UpdateUserAsync(It.IsAny<UpdateUserViewModel>()) as ObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }
    }
}