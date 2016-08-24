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
                            Key = "1",
                            Name = "Test Firearm",
                            SerialNumber = "001",
                            IsNfaRegistered = false
                        }
                   };
            _singleFirearm = new Firearm { Key = "1", Name = "Test Firearm", SerialNumber = "001", IsNfaRegistered = false };
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
            Assert.Equal("1", result.First().Key);
        }

        [Fact]
        public void ReturnDefaultFirearmWhenIdIsPassedToGetById()
        {
            SetupRepository(x => x.Find(It.IsAny<string>()), _singleFirearm);
            var result = _controller.GetById("1") as ObjectResult;
            Assert.Equal("1", ((Firearm)result.Value).Key);
        }

        [Fact]
        public void ReturnNotFoundWhenInvalidKeyPassedToGetById()
        {
            SetupRepository(x => x.Find(It.IsAny<string>()), null);
            var result = _controller.GetById("-1");
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void ReturnBadRequestWhenNullIsPassedToCreate()
        {
            var controller = new FirearmController(_repo);
            var result = controller.Create(null);
            Assert.IsType<BadRequestResult>(result);
        }


    }
}
