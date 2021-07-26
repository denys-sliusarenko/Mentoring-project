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
        public void GetUsers_WhenGetAllUserFromDatabase_ReturnAllUsers()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var controller = new UsersController(userService, _mapper);
                var expectedUsers = context.Users;

                // Act
                var result = controller.GetUsers() as OkObjectResult;

                // Assert
                Assert.NotNull(result);
                Assert.Equal(200, result.StatusCode);
                result.Value.Should().BeEquivalentTo(expectedUsers);
            }
        }
    }
}
