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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataSavingAppTest.Controllers
{
    public class AddDataControllerTest
    {
        private readonly AddDataController _controller;
        private readonly Mock<IAddDataService> _addDataService;
        private readonly Fixture _fixture = new Fixture();
        public AddDataControllerTest()
        {
            _addDataService = new Mock<IAddDataService>();
            _controller = new AddDataController(_addDataService.Object);
        }
        [Fact]
        public void Given_valid_File_upload_Returns_200Ok()
        {
            //Arrange
            var uploadrequest = _fixture.Create<DataSavingRepo>();
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy file")), 0, 0, "Data", "excel.xlsx");

            //Act
            _addDataService.Setup(x => x.AddData(file)).Returns(true);
            var result = _controller.importExcel(file) as OkResult;

            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
        }
        [Fact]
        public void Given_valid_File_upload_Returns_400BadRequest()
        {
            //Arrange
            var uploadrequest = _fixture.Create<DataSavingRepo>();
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy file")), 0, 0, "Data", "excel.xlsx");

            //Act
            _addDataService.Setup(x => x.AddData(file)).Returns(false);
            var result = _controller.importExcel(file) as BadRequestResult;

            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
        }
        [Fact]
        public void Given_valid_File_upload_Returns_500InternalServer_error()
        {
            //Arrange
            var errorMessage = _fixture.Create<string>();
            IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy file")), 0, 0, "Data", "excel.xlsx");

            //Act
            _addDataService.Setup(x => x.AddData(file)).Throws(new Exception(errorMessage));
            var result = _controller.importExcel(file) as ObjectResult;

            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}
