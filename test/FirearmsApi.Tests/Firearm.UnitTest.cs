using fadb_api.Controllers;
using fadb_api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;

namespace FirearmsApi.Tests
{
    public class Firearm_API_Should
    {
        private readonly IFirearmRepository _repo;
        private Mock<IFirearmRepository> _mockRepo;
        private List<Firearm> _firearmCollection;
        private Firearm _singleFirearm;
        private FirearmController _controller;

        public Firearm_API_Should()
        {
            _mockRepo = new Mock<IFirearmRepository>();
            _repo = _mockRepo.Object;
            _firearmCollection = new List<Firearm>
                   {
                        new Firearm
                        {
                            Id = 1,
                            Name = "Test Firearm",
                            SerialNumber = "001",
                            IsNfaRegistered = false
                        }
                   };
            _singleFirearm = new Firearm { Id = 1, Name = "Test Firearm", SerialNumber = "001", IsNfaRegistered = false };
        }

        private void SetupRepository<T>(Expression<Func<IFirearmRepository, T>> method, T returnObj)
        {
            _mockRepo.Setup(method).Returns(returnObj);
            _controller = new FirearmController(_repo);
        }

        [Fact]
        public void ReturnDefaultFirearmWhenGetAllIsCalled()
        {
            SetupRepository(x => x.GetAll(), _firearmCollection);
            var result = _controller.GetAll();
            Assert.Equal(1, result.First().Id);
        }

        [Fact]
        public void ReturnDefaultFirearmWhenIdIsPassedToGetById()
        {
            SetupRepository(x => x.Find(It.IsAny<int>()), _singleFirearm);
            var result = _controller.GetById(1) as ObjectResult;
            Assert.Equal(1, ((Firearm)result.Value).Id);
        }

        [Fact]
        public void ReturnNotFoundWhenInvalidKeyPassedToGetById()
        {
            SetupRepository(x => x.Find(It.IsAny<int>()), null);
            var result = _controller.GetById(-1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ReturnBadRequestWhenNullIsPassedToCreate()
        {
            var controller = new FirearmController(_repo);
            var result = controller.Create(null);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ReturnSuccessResultWhenFirearmDataCreated()
        {
            var controller = new FirearmController(_repo);
            var firearm = new Firearm { Name = "Test", SerialNumber = "001", IsNfaRegistered = true };
            var result = controller.Create(firearm);
            Assert.IsType<CreatedAtRouteResult>(result);
            var routeResult = result as CreatedAtRouteResult;
            Assert.NotNull(routeResult);
            Assert.Equal(201, routeResult.StatusCode);                      
        }

        [Fact]
        public void ReturnBadRequestWhenIdIsNullOnPutRequest()
        {
            var controller = new FirearmController(_repo);
            var firearm = new Firearm { Id = 123, Name = "Test", SerialNumber = "001", IsNfaRegistered = true };
            var result = controller.Update(-1, firearm);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ReturnBadRequestWhenFirearmIsNullOnPutRequest()
        {
            var controller = new FirearmController(_repo);
            var result = controller.Update(-1, null);
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void ReturnNotFoundWhenFirearmToUpdateDoesntExist()
        {
            SetupRepository(x => x.Find(It.IsAny<int>()), null);
            var firearm = new Firearm { Id = 123, Name = "Test", SerialNumber = "001", IsNfaRegistered = true };
            var result = _controller.Update(123, firearm);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Return204ResultWhenFirearmDataUpdated()
        {
            var firearm = new Firearm { Id = 1, Name = "Test", SerialNumber = "001", IsNfaRegistered = true };
            SetupRepository(x => x.Find(It.IsAny<int>()), firearm);           
            var result = _controller.Update(1, firearm);
            Assert.IsType<NoContentResult>(result);
            var routeResult = result as NoContentResult;
            Assert.NotNull(routeResult);
            Assert.Equal(204, routeResult.StatusCode);
        }

        [Fact]
        public void ReturnNotFoundWhenFirearmToDeleteDoesntExist()
        {
            SetupRepository(x => x.Find(It.IsAny<int>()), null);
            var result = _controller.Delete(1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Return204ResultWhenFirearmDataDeleted()
        {
            var firearm = new Firearm { Id = 1, Name = "Test", SerialNumber = "001", IsNfaRegistered = true };
            SetupRepository(x => x.Find(It.IsAny<int>()), firearm);
            var result = _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
            var routeResult = result as NoContentResult;
            Assert.NotNull(routeResult);
            Assert.Equal(204, routeResult.StatusCode);
        }
    }
}
