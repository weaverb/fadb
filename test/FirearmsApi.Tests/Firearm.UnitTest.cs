using fadb_api.Controllers;
using fadb_api.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FirearmsApi.Tests
{
    public class Firearm_API_Should
    {
        private readonly IFirearmRepository _repo;
        private Mock<IFirearmRepository> _mockRepo;
        private List<Firearm> _firearmCollection;
        private Firearm _singleFirearm;

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

        [Fact]
        public void ReturnDefaultFirearmWhenGetAllIsCalled()
        {
            _mockRepo.Setup(x => x.GetAll()).Returns(_firearmCollection);
            var controller = new FirearmController(_repo);
            var result = controller.GetAll();
            Assert.Equal("1", result.First().Key);
        }

        [Fact]
        public void ReturnDefaultFirearmWhenIdIsPassedToGetById()
        {
            _mockRepo.Setup(x => x.Find(It.IsAny<string>())).Returns(_singleFirearm);
            var controller = new FirearmController(_repo);
            var result = controller.GetById("1") as ObjectResult;
            Assert.Equal("1", ((Firearm)result.Value).Key);
        }

        [Fact]
        public void ReturnNotFoundWhenInvalidKeyPassedToGetById()
        {
            _mockRepo.Setup(x => x.Find(It.IsAny<string>()));
            var controller = new FirearmController(_repo);
            var result = controller.GetById("-1");
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
