using Mentoring_project.Business.Services;
using Mentoring_project.Domain.Core.Entities;
using Mentoring_project.Infrastructure.Data.Data;
using MentoringProject.Tests;
using System;
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
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);
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
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);
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
        public async Task CreateUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);
                int expectedCount = context.Users.Count() + 1;

                // Act
                var newUser = new User()
                {
                    UserId = 0,
                    FirstName = "Homer",
                    LastName = "Simpson"
                };

                await userService.CreateUser(newUser);
                var allUsers = context.Users;

                //Assert
                Assert.Equal(expectedCount, allUsers.Count());

                Assert.Contains(newUser, allUsers);
            }
        }

        [Fact]
        public async Task DeleteUserTest()
        {
            using (var context = new DbProjectContext(_fixture.GetDbOptions()))
            {
                //Arrange
                var uow = new UnitOfWork(context);
                var userService = new UserService(uow);
                int idDeleteUser = context.Users.First().UserId;
                int expectedCount = context.Users.Count() - 1;

                // Act
                await userService.DeleteUser(idDeleteUser);
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
                var userService = new UserService(uow);
                int idDeleteUser = int.MinValue;
                int expectedCount = context.Users.Count();

                // Act
                await userService.DeleteUser(idDeleteUser);
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

                var userService = new UserService(uow);

                // Act
                var existUser = context.Users.First();

                //Assert
                await Assert.ThrowsAsync<ArgumentException>(async () => await userService.CreateUser(existUser));
            }
        }

        [Fact]
        public async Task UpdateUserTest()
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

                await userService.UpdateUser(updateUser);
                var savedUser = await context.Users.FindAsync(updateUser.UserId);

                //Assert
                Assert.Equal(updateUser, savedUser);
            }
        }

    }
}