﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using MentoringProject.Application.DTO;
using MentoringProject.Application.Interfaces;
using MentoringProject.Controllers;
using MentoringProject.Domain.Core.Exceptions;
using MentoringProject.TestDataConfiguration;
using MentoringProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MentoringProject.Web.Tests
{
    public class OwnersControllerTest : IClassFixture<TestDataFixture>
    {
        private readonly IMapper _mapper;
        private readonly TestDataFixture _testDataFixture;

        public OwnersControllerTest(TestDataFixture testDataFixture)
        {
            _mapper = MapperConfig.GetMapper();
            _testDataFixture = testDataFixture;
        }

        [Fact]
        public void Get_WhenGetAllOwnersFromDatabase_ReturnAllOwnersStatusOk()
        {
            // Arrange
            var expectedOwners = _testDataFixture.GetTestDtoOwners();
            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.GetAll()).Returns(expectedOwners);

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            var result = controller.Get() as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            result.Value.Should().BeEquivalentTo(expectedOwners);
        }

        [Fact]
        public async Task Get_WhenOwnerExist_ReturnOwnerStatusOk()
        {
            // Arrange
            var expectedOwner = _testDataFixture.GetTestDtoOwners().First();

            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.GetAsync(It.IsAny<int>())).ReturnsAsync(expectedOwner);

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            var result = await controller.Get(It.IsAny<int>()) as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            result.Value.Should().BeEquivalentTo(expectedOwner);
        }

        [Fact]
        public void Get_WhenOwnerNotExist_ReturnNotFoundStatus()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.GetAsync(It.IsAny<int>())).Throws(new NotFoundException());

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            async Task ActAsync() => await controller.Get(It.IsAny<int>());

            // Assert
            Assert.ThrowsAsync<NotFoundException>(ActAsync);
        }

        [Fact]
        public async Task CreateAsync_CreateCorrectOwner_ReturnCreatedOwnerStatusOk()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.CreateAsync(It.IsAny<OwnerDTO>()))
            .ReturnsAsync(_testDataFixture.GetTestDtoOwners().First());

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            var result = await controller.CreateAsync(It.IsAny<CreateOwnerViewModel>()) as ObjectResult;

            // Assert
            Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(StatusCodes.Status201Created, result.StatusCode);
        }

        [Fact]
        public async Task DeleteAsync_WhenOwnerExist_ReturnStatusNoContent()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            var result = await controller.DeleteAsync(It.IsAny<int>()) as StatusCodeResult;

            // Assert
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(StatusCodes.Status204NoContent, result.StatusCode);
        }

        [Fact]
        public void DeleteAsync_WhenOwnerNotExist_ReturnStatusNoFound()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.GetAsync(It.IsAny<int>())).ReturnsAsync((OwnerDTO)null);
            ownerServiceMock.Setup(repo => repo.DeleteAsync(It.IsAny<int>())).Throws(new NotFoundException());

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            Task Act() => controller.DeleteAsync(It.IsAny<int>());

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Act);
        }

        [Fact]
        public async Task UpdateAsync_WhenUserExist_ReturnUpdatedOwnerStatusOk()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.UpdateAsync(It.IsAny<OwnerDTO>())).ReturnsAsync(_testDataFixture.GetTestDtoOwners().First());

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            var result = await controller.UpdateAsync(It.IsAny<UpdateOwnerViewModel>()) as ObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public void UpdateAsync_WhenOwnerNotExist_ReturnNotFoundObjectResult()
        {
            // Arrange
            var ownerServiceMock = new Mock<IOwnerService>();
            ownerServiceMock.Setup(repo => repo.UpdateAsync(It.IsAny<OwnerDTO>())).Throws(new NotFoundException());

            var controller = new OwnersController(ownerServiceMock.Object, _mapper);

            // Act
            async Task Act() => await controller.UpdateAsync(It.IsAny<UpdateOwnerViewModel>());

            // Assert
            Assert.ThrowsAsync<NotFoundException>(Act);
        }
    }
}