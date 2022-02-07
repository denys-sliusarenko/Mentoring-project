using System.Collections.Generic;
using MentoringProject.Application.DTO;
using MentoringProject.Domain.Core.Entities;

namespace MentoringProject.TestDataConfiguration
{
    public class TestDataFixture
    {
        public List<Owner> GetTestOwners()
        {
            var owners = new List<Owner>
            {
                new Owner
                {
                    Id = 1,
                    FirstName = "Tom",
                    LastName = "Walker",
                },
                new Owner
                {
                    Id = 2,
                    FirstName = "Alice",
                    LastName = "Walker",
                },
                new Owner
                {
                    Id = 3,
                    FirstName = "Sam",
                    LastName = "Walker",
                },
            };
            return owners;
        }

        public List<OwnerDTO> GetTestDtoOwners()
        {
            var owners = new List<OwnerDTO>
            {
                new OwnerDTO
                {
                    Id = 1,
                    FirstName = "Tom",
                    LastName = "Walker",
                },
                new OwnerDTO
                {
                    Id = 2,
                    FirstName = "Alice",
                    LastName = "Walker",
                },
                new OwnerDTO
                {
                    Id = 3,
                    FirstName = "Sam",
                    LastName = "Walker",
                },
            };
            return owners;
        }
    }
}
