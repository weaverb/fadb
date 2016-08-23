using fadb_api.Controllers;
using fadb_api.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace FirearmsApi.Tests
{
    public class Firearm_API_Should
    {
        private readonly IFirearmRepository _repo;
        private Mock<IFirearmRepository> _mockRepo;

        public Firearm_API_Should()
        {
            _mockRepo = new Mock<IFirearmRepository>();
            _repo = _mockRepo.Object;
        }

        [Fact]
        public void ReturnDefaultFirearmWhenGetAllIsCalled()
        {
            _mockRepo.Setup(x => x.GetAll())
                    .Returns(new List<Firearm>
                    {
                        new Firearm
                        {
                            Key = "1",
                            Name = "Test Firearm",
                            SerialNumber = "001",
                            IsNfaRegistered = false
                        }
                    });

            var controller = new FirearmController(_repo);
            var result = controller.GetAll();

            Assert.Equal("1", result.First().Key);

        }


    }
}
