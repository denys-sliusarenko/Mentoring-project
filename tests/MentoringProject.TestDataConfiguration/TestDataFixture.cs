using MentoringProject.Application.DTO;
using MentoringProject.Domain.Core.Entities;
using MentoringProject.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace MentoringProject.TestDataConfiguration
{
    public class TestDataFixture
    {
        public List<User> GetTestUsers()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    FirstName = "Tom",
                    LastName = "Walker",
                },
                new User
                {
                    Id = 2,
                    FirstName = "Alice",
                    LastName = "Walker",
                },
                new User
                {
                    Id = 3,
                    FirstName = "Sam",
                    LastName = "Walker",
                },
            };
            return users;
        }

        public List<UserDTO> GetTestDTOUsers()
        {
            var users = new List<UserDTO>
            {
                new UserDTO
                {
                    Id = 1,
                    FirstName = "Tom",
                    LastName = "Walker",
                },
                new UserDTO
                {
                    Id = 2,
                    FirstName = "Alice",
                    LastName = "Walker",
                },
                new UserDTO
                {
                    Id = 3,
                    FirstName = "Sam",
                    LastName = "Walker",
                },
            };
            return users;
        }
    }
}
