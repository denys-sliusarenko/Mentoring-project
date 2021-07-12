using AutoMapper;
using Mentoring_project.Business.DTO;
using Mentoring_project.Business.Services;
using Mentoring_project.Domain.Core.Entities;
using Mentoring_project.Infrastructure.Data.Data;
using Mentoring_project.Mapper;
using MentoringProject.Tests;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Mentoring_project.Test
{
    public class UserServiceTest : IClassFixture<DbFixture>
    {
        private DbFixture _fixture;
        private readonly IMapper _mapper;

        public UserServiceTest(DbFixture fixture)
        {
            _fixture = fixture;
            if (_mapper == null)
            {
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                IMapper mapper = mappingConfig.CreateMapper();
                _mapper = mapper;
            }
        }

        [Fact]
        public void GetUserByIdTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                var expectedUser = _mapper.Map<UserDTO>(context.Users.First());

                // Act
                var result = userService.GetUserById(expectedUser.UserId);
                var obj1 = JsonConvert.SerializeObject(result);
                var obj2 = JsonConvert.SerializeObject(expectedUser);

                //Assert
                Assert.Equal(obj1, obj2);
            }
        }

        [Fact]
        public void GetNotExistUserByIdTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int IdUser = int.MinValue;

                // Act
                var result = userService.GetUserById(IdUser);

                //Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public void GetAllUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int expectedCount = context.Users.Count();

                // Act
                var result = userService.GetAll();

                //Assert
                Assert.Equal(expectedCount, result.Count());
            }
        }

        [Fact]
        public async Task CreateUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int expectedCount = context.Users.Count() + 1;

                // Act
                var newUser = new UserDTO()
                {
                    UserId = 0,
                    FirstName = "Homer",
                    LastName = "Simpson"
                };

                await userService.CreateUserAsync(newUser);
                var allUsers = _mapper.Map<IEnumerable<UserDTO>>(context.Users);

                //Assert
                Assert.Equal(expectedCount, allUsers.Count());

                Assert.Contains(allUsers, p => p.FirstName == newUser.FirstName && p.LastName == newUser.LastName);
            }
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int idDeleteUser = context.Users.First().UserId;
                int expectedCount = context.Users.Count() - 1;

                // Act
                await userService.DeleteUserAsync(idDeleteUser);
                var allUsers = context.Users;

                //Assert
                Assert.Equal(expectedCount, allUsers.Count());
                Assert.DoesNotContain(allUsers, u => u.UserId == idDeleteUser);
            }
        }
        [Fact]
        public async Task DeleteNotExistUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int idDeleteUser = int.MinValue;
                int expectedCount = context.Users.Count();

                // Act
                await userService.DeleteUserAsync(idDeleteUser);
                var allUsers = context.Users;

                //Assert
                Assert.Equal(expectedCount, allUsers.Count());
            }
        }

        [Fact]
        public async Task CreateExistUserExceptionTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);

                var userService = new UserService(uow, _mapper);

                // Act
                var existUser = _mapper.Map<UserDTO>(context.Users.First());

                //Assert
                await Assert.ThrowsAsync<InvalidOperationException>(async () => await userService.CreateUserAsync(existUser));
            }
        }

        [Fact]
        public async Task UpdateUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);

                // Act
                var updateUser = _mapper.Map<UserDTO>(context.Users.AsNoTracking().First());

                updateUser.FirstName = Guid.NewGuid().ToString();
                updateUser.LastName = Guid.NewGuid().ToString();

                await userService.UpdateUserAsync(updateUser);
                var savedUser = _mapper.Map<UserDTO>(await context.Users.FindAsync(updateUser.UserId));

                //Assert
                var obj1 = JsonConvert.SerializeObject(updateUser);
                var obj2 = JsonConvert.SerializeObject(savedUser);

                Assert.Equal(obj1, obj2);
            }
        }
    }
}