using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Services;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.Domain.Core.Interfaces.Repositories;
using MentoringProject.Domain.Entities;
using MentoringProject.TestDataConfiguration;
using Moq;
using Xunit;

namespace MentoringProject.Web.Tests
{
    public class OwnerServiceTest : IClassFixture<TestDataFixture>
    {
        private readonly IMapper _mapper;
        private readonly TestDataFixture _testDataFixture;

        public OwnerServiceTest(TestDataFixture testDataFixture)
        {
            _mapper = MapperConfig.GetMapper();
            _testDataFixture = testDataFixture;
        }

        [Fact]
        public void Get_WhenOwnerExists_ReturnCorrectOwner()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.GetAsync(It.IsAny<Guid>())).ReturnsAsync(_testDataFixture.GetTestOwners().First());
            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            var result = ownerService.GetAsync(It.IsAny<Guid>());

            // Assert
            unitOfWorkMock.Verify(u => u.OwnerRepository.GetAsync(It.IsAny<Guid>()));
        }

        [Fact]
        public void Get_WhenNotExists_ThrowUserException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.GetAsync(It.IsAny<Guid>())).Throws(new NotFoundException());
            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            async Task Act() => await ownerService.GetAsync(It.IsAny<Guid>());

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Act);
        }

        [Fact]
        public void Get_WhenGetAllOwnersFromDatabase_ReturnAllOwners()
        {
            // Arrange
            var allOwners = _testDataFixture.GetTestOwners();
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.GetAll()).Returns(allOwners);
            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            var result = ownerService.GetAll();

            // Assert
            unitOfWorkMock.Verify(u => u.OwnerRepository.GetAll());

            result.Should().BeEquivalentTo(allOwners);
        }

        [Fact]
        public async Task CreateAsync_CreateCorrectOwner_ReturnCreatedOwner()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.Create(It.IsAny<Owner>()));

            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            var createdOwner = await ownerService.CreateAsync(It.IsAny<OwnerDTO>());

            // Assert
            unitOfWorkMock.Verify(u => u.OwnerRepository.Create(It.IsAny<Owner>()));
        }

        [Fact]
        public async Task DeleteAsync_WhenExistOwner_RemovedOwner()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.GetAsync(It.IsAny<Guid>())).ReturnsAsync(_testDataFixture.GetTestOwners().First());
            unitOfWorkMock.Setup(u => u.OwnerRepository.Delete(It.IsAny<Guid>()));

            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            await ownerService.DeleteAsync(It.IsAny<Guid>());

            // Assert
            unitOfWorkMock.Verify(u => u.OwnerRepository.Delete(It.IsAny<Guid>()));
        }

        [Fact]
        public async Task DeleteOwnerAsync_WhenNotExistOwner_ThrowsNotFoundException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.GetAsync(It.IsAny<Guid>())).ReturnsAsync((Owner)null);

            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            Task Act() => ownerService.DeleteAsync(It.IsAny<Guid>());

            // Assert
            await Assert.ThrowsAsync<NotFoundException>(Act);
        }

        [Fact]
        public async Task UpdateOwnerAsync_WhenOwnerExist_ReturnUpdatedOwner()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.Exist(It.IsAny<Guid>())).ReturnsAsync(true);
            unitOfWorkMock.Setup(u => u.OwnerRepository.Update(It.IsAny<Owner>())).Verifiable();

            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            var updatedOwner = await ownerService.UpdateAsync(_testDataFixture.GetTestDtoOwners().First());

            // Assert
            unitOfWorkMock.Verify(u => u.OwnerRepository.Update(It.IsAny<Owner>()));
            updatedOwner.Should().BeEquivalentTo(_testDataFixture.GetTestDtoOwners().First());
        }

        [Fact]
        public void UpdateOwnerAsync_WhenOwnerNotExist_ThrowsNotFoundException()
        {
            // Arrange
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(u => u.OwnerRepository.Exist(It.IsAny<Guid>())).ReturnsAsync(false);

            var ownerService = new OwnerService(unitOfWorkMock.Object, _mapper);

            // Act
            async Task Act() => await ownerService.UpdateAsync(_testDataFixture.GetTestDtoOwners().First());

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Act);
        }
    }
}