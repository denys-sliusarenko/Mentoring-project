using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Services;
using MentoringProject.Controllers;
using MentoringProject.Infrastructure.Data;
using MentoringProject.TestDataConfiguration;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MentoringProject.Web.Tests
{
    public class UsersControllerTest : IClassFixture<DbFixture>, IDisposable
    {
        private readonly DbProjectContext _context;
        private readonly UsersController _controller;

        public UsersControllerTest(DbFixture dbFixture)
        {
            var mapper = MapperConfig.GetMapper();
            _context = new DbProjectContext(dbFixture.GetDbOptions());
            var uow = new UnitOfWork(_context);
            var userService = new UserService(uow, mapper);
            _controller = new UsersController(userService, mapper);
        }

        void IDisposable.Dispose()
        {
            _context.Dispose();
        }

        [Fact]
        public void GetUsers_WhenGetAllUserFromDatabase_ReturnAllUsersStatusOk()
        {
            // Arrange
            var expectedUsers = _context.Users;

            // Act
            var result = _controller.GetUsers() as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            result.Value.Should().BeEquivalentTo(expectedUsers);
        }

        [Fact]
        public void GetUserById_WhenUserExist_ReturnUserStatusOk()
        {
            // Arrange
            var expectedUser = _context.Users.First();

            // Act
            var result = _controller.GetUserById(expectedUser.UserId) as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            result.Value.Should().BeEquivalentTo(expectedUser);
        }

        [Fact]
        public void GetUserById_WhenUserNotExist_ReturnNotFoundStatus()
        {
            // Arrange
            int idUser = int.MinValue;

            // Act
            var result = _controller.GetUserById(idUser) as ObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_CreateCorrectUser_ReturnCreatedUserStatusOk()
        {
            // Arrange
            var newUser = new CreateUserViewModel()
            {
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
            };

            // Act
            var result = await _controller.CreateUserAsync(newUser) as ObjectResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
            newUser.Should().BeEquivalentTo((UserDTO)result.Value, options => options.Excluding(info => info.UserId));
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserExist_ReturnStatusNoContent()
        {
            // Arrange
            var idUser = _context.Users.First().UserId;

            // Act
            var result = await _controller.DeleteUserAsync(idUser) as StatusCodeResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserNotExist_ReturnStatusNoFound()
        {
            // Arrange
            var idUser = int.MinValue;

            // Act
            var result = await _controller.DeleteUserAsync(idUser) as ObjectResult;

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExist_ReturnUpdatedUserStatusOk()
        {
            // Arrange
            var user = new UpdateUserViewModel()
            {
                UserId = _context.Users.AsNoTracking().First().UserId,
                FirstName = Guid.NewGuid().ToString(),
                LastName = Guid.NewGuid().ToString(),
            };

            // Act
            var result = await _controller.UpdateUserAsync(user) as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            user.Should().BeEquivalentTo(result.Value);
        }
    }
}