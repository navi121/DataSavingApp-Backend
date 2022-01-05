using AutoFixture;
using DataSavingApplication.Controllers;
using DataSavingApplication.Models;
using DataSavingApplication.Service.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataSavingAppTest.Controllers
{
    public class GetDataControllerTest
    {
        private readonly Mock<IGetDataService> _getDataService;
        private readonly GetDataController _controller;
        private readonly Fixture _fixture = new Fixture();

        public GetDataControllerTest()
        {
            _getDataService = new Mock<IGetDataService>();
            _controller = new GetDataController(_getDataService.Object);
        }

        [Fact]
        public async Task Get_All_Data_from_DB()
        {
            //Act
            var result = _controller.GetAllData() as OkObjectResult;
            var expectedResult = ((IEnumerable<DataSavingModel>)result.Value).ToList();

            //Assert
            Assert.Equal(_getDataService.Object.GetDatas().Result, expectedResult);
        }

        [Fact]
        public void Get_All_Data_from_DB_Returns_500_internal_Server_Error()
        {
            //Arrange
            var errorMessage = _fixture.Create<string>();

            //Act
            _getDataService.Setup(x => x.GetDatas()).Throws(new Exception(errorMessage));
            var result = _controller.GetAllData() as ObjectResult;

            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
