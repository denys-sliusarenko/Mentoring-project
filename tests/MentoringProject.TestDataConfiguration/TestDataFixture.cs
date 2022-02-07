using System;
using System.Collections.Generic;
using MentoringProject.Application.DTO;
using MentoringProject.Domain.Entities;

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
                    Id = Guid.NewGuid(),
                    FirstName = "Tom",
                    LastName = "Walker",
                    OwnersCars = new  List<OwnerCar>(),

                },
                new Owner
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alice",
                    LastName = "Walker",
                    OwnersCars = new  List<OwnerCar>(),

                },
                new Owner
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Sam",
                    LastName = "Walker",
                    OwnersCars = new List<OwnerCar>(),
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
                    Id = Guid.NewGuid(),
                    FirstName = "Tom",
                    LastName = "Walker",
                    OwnersCars = new List<OwnerCarDTO>(),
                },
                new OwnerDTO
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Alice",
                    LastName = "Walker",
                    OwnersCars = new List<OwnerCarDTO>(),

                },
                new OwnerDTO
                {
                    Id = Guid.NewGuid(),
                    FirstName = "Sam",
                    LastName = "Walker",
                    OwnersCars = new List<OwnerCarDTO>(),

                },
            };
            return owners;
        }
    }
}
