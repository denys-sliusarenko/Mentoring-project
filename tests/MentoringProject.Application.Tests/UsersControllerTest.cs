using AutoMapper;
using MentoringProject.Infrastructure.Data;
using MentoringProject.Mapper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MentoringProject.TestDataConfiguration;
using MentoringProject.Controllers;
using MentoringProject.Application.Services;
using Microsoft.AspNetCore.Mvc;
using MentoringProject.Application.DTO;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using MentoringProject.ViewModels;
using FluentAssertions.Equivalency;
using Microsoft.EntityFrameworkCore;

namespace MentoringProject.Web.Tests
{
    public class UsersControllerTest : IClassFixture<DbFixture>
    {
        private DbFixture _dbFixture;
        private readonly IMapper _mapper;
        public UsersControllerTest(DbFixture dbFixture)
        {
            _dbFixture = dbFixture;
            _mapper = MapperConfig.GetMapper();
        }

        [Fact]
        public void GetUsers_WhenGetAllUserFromDatabase_ReturnAllUsersStatusOk()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);
                var expectedUsers = context.Users;

                // Act
                var result = controller.GetUsers() as ObjectResult;

                // Assert
                Assert.IsType<OkObjectResult>(result);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
                result.Value.Should().BeEquivalentTo(expectedUsers);
            }
        }

        [Fact]
        public void GetUserById_WhenUserExist_ReturnUserStatusOk()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                // Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);
                var expectedUser = context.Users.First();

                // Act
                var result = controller.GetUserById(expectedUser.UserId) as ObjectResult;

                // Assert
                Assert.IsType<OkObjectResult>(result);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
                result.Value.Should().BeEquivalentTo(expectedUser);
            }
        }

        [Fact]
        public void GetUserById_WhenUserNotExist_ReturnNotFoundStatus()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                // Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);
                int IdUser = int.MinValue;

                // Act
                var result = controller.GetUserById(IdUser) as ObjectResult;

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            }
        }

        [Fact]
        public async Task CreateUserAsync_CreateCorrectUser_ReturnCreatedUserStatusOk()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                // Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);

                var newUser = new CreateUserViewModel()
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };

                //Act
                var result = await controller.CreateUserAsync(newUser) as ObjectResult;

                //Assert
                Assert.IsType<CreatedAtActionResult>(result);
                Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
                newUser.Should().BeEquivalentTo((UserDTO)result.Value, options => options.Excluding(info => info.UserId));
            }
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserExist_ReturnStatusNoContent()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                // Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);
                var idUser = context.Users.First().UserId;

                //Act
                var result = await controller.DeleteUserAsync(idUser) as StatusCodeResult;

                //Assert
                Assert.IsType<NoContentResult>(result);
                Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
            }
        }

        [Fact]
        public async Task DeleteUserAsync_WhenUserNotExist_ReturnStatusNoFound()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                // Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);
                var idUser = int.MinValue;

                //Act
                var result = await controller.DeleteUserAsync(idUser) as ObjectResult;

                //Assert
                Assert.IsType<NotFoundObjectResult>(result);
                Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
            }
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserExist_ReturnUpdatedUserStatusOk()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                // Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);

                var user = new UpdateUserViewModel()
                {
                    UserId = context.Users.AsNoTracking().First().UserId,
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };

                //Act
                var result = await controller.UpdateUserAsync(user) as ObjectResult;

                //Assert
                Assert.IsType<OkObjectResult>(result);
                Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
                user.Should().BeEquivalentTo(result.Value);
            }
        }
    }
}