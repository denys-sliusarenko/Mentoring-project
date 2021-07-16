using AutoMapper;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Services;
using MentoringProject.Infrastructure.Data.Data;
using MentoringProject.Mapper;
using MentoringProject.Tests;
using Microsoft.EntityFrameworkCore;
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
        private DbFixture _dbFixture;
        private readonly IMapper _mapper;

        public UserServiceTest(DbFixture dbFixture)
        {
            _dbFixture = dbFixture;
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
        public void GetNotExistUserByIdTest()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
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
        public async Task CreateUserTest()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);

                // Act
                var newUser = new UserDTO()
                {
                    FirstName = Guid.NewGuid().ToString(),
                    LastName = Guid.NewGuid().ToString()
                };

                var createdUser = await userService.CreateUserAsync(newUser);
                var allUsers = _mapper.Map<IEnumerable<UserDTO>>(context.Users);

                //Assert
                allUsers.Should().ContainEquivalentOf(createdUser);
            }
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow, _mapper);
                int idUser = context.Users.First().UserId;

                // Act
                await userService.DeleteUserAsync(idUser);
                var isDeleted = context.Users.Find(idUser);

                //Assert
                Assert.Null(isDeleted);
            }
        }

        [Fact]
        public async Task DeleteNotExistUserTest()
        {
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
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
            using (var context = new DbProjectContext(_dbFixture.GetDbOptions()))
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
    }
}