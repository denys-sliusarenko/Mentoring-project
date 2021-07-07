using Mentoring_project.Entities;
using Mentoring_project.Interfaces;
using Mentoring_project.Repositories;
using Mentoring_project.Services;
using MentoringProject.Tests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
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
        public UserServiceTest(DbFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void GetUserByIdTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new Mock<IUnitOfWork>();
                var userRepositoryMock = new Mock<UserRepository>(context);
                uow.Setup(repo => repo.UserRepository).Returns(userRepositoryMock.Object);

                var userService = new UserService(uow.Object);
                var expectedUser = context.Users.First();
               
                // Act
                var result = userService.GetUserById(expectedUser.UserId);

                //Assert
                Assert.Equal(expectedUser, result);
            }
        }

        [Fact]
        public void GetNotExistUserByIdTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new Mock<IUnitOfWork>();
                var userRepositoryMock = new Mock<UserRepository>(context);
                uow.Setup(repo => repo.UserRepository).Returns(userRepositoryMock.Object);

                var userService = new UserService(uow.Object);
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
                var userService = new UserService(uow);
                int expectedCount = context.Users.Count();

                // Act
                var result = userService.GetAll();

                //Assert
                Assert.Equal(expectedCount, result.Count());
            }
        }

        [Fact]
        public void CreateUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);
                int expectedCount = context.Users.Count()+1;

                // Act
                var newUser = new User()
                {
                    UserId = 4,
                    FirstName = "Homer",
                    LastName = "Simpson"
                };

                userService.AddUser(newUser).GetAwaiter().GetResult();
                var result = userService.GetAll();

                //Assert
                Assert.Equal(expectedCount, result.Count());

                Assert.Contains(newUser, result);
            }
        }

        [Fact]
        public void DeleteUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);
                int idDeleteUser = context.Users.First().UserId;
                int expectedCount = context.Users.Count() - 1;

                // Act
                userService.DeleteUser(idDeleteUser).GetAwaiter().GetResult();
                var result = userService.GetAll();

                //Assert
                Assert.Equal(expectedCount, result.Count());
                Assert.DoesNotContain(result, u => u.UserId == idDeleteUser);
            }
        }
        [Fact]
        public void DeleteNotExistUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);
                int idDeleteUser = int.MinValue;
                int expectedCount = context.Users.Count();

                // Act
                userService.DeleteUser(idDeleteUser).GetAwaiter().GetResult();
                var result = userService.GetAll();

                //Assert
                Assert.Equal(expectedCount, result.Count());
            }
        }

        [Fact]
        public void CreateExistUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);

                var userService = new UserService(uow);

                // Act
                var existUser = context.Users.First();

                //Assert
                Assert.Throws<ArgumentException>(() => userService.AddUser(existUser).GetAwaiter().GetResult());
            }
        }

        [Fact]
        public void UpdateUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);

                // Act
                var updateUser = context.Users.First();
                updateUser.FirstName = Guid.NewGuid().ToString();
                updateUser.LastName = Guid.NewGuid().ToString();

                userService.UpdateUser(updateUser).GetAwaiter().GetResult();
                var savedUser = context.Users.First();

                //Assert
                Assert.Equal(updateUser, savedUser);
            }
        }

    }
}
