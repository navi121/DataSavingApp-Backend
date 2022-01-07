using AutoFixture;
using DataSavingApplication.Models;
using DataSavingApplication.Service;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataSavingAppTest.Services
{
    public class AddDataServiceTest
    {
        private readonly DataSavingRepo _dataSavingRepo;
        private readonly AddDataService _addDataService;
        private readonly Fixture _fixture = new Fixture();
        public AddDataServiceTest()
        {
            _dataSavingRepo = new DataSavingRepo();
            _addDataService = new AddDataService();
        }
        [Fact]
        public void Given_Valid_FileUpload_Service_SaveIn_Db_Return_True()
        {
            //Arrange 
            var uploadrequest = _fixture.Create<DataSavingRepo>();
            //IFormFile file = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy file")), 0, 0, "Data", "excel.xlsx").ContentType("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            //var content = File.OpenRead(@"C:\Users\NaveenNatarajan\Downloads\SampleExcel.xlsx");
            
            using(var stream = File.OpenRead(@"C:\Users\NaveenNatarajan\Downloads\SampleExcel.xlsx"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"C:\Users\NaveenNatarajan\Downloads\SampleExcel.xlsx"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
                };
                 var result = _addDataService.AddData(file);
                Assert.True(result);
            }
            //Act
            //file.Setup(x => x.OpenReadStream()).Returns(content);
            //file.Setup(x => x.ContentType).Returns("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");         
        }

        [Fact]
        public void Given_Valid_FileUpload_Service_Return_False()
        {
            using (var stream = File.OpenRead(@"C:\Users\NaveenNatarajan\Downloads\6.jpeg"))
            {
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(@"C:\Users\NaveenNatarajan\Downloads\6.jpeg"))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image.jpg"
                };
                var result = _addDataService.AddData(file);

                Assert.False(result);
            }
        }
    }
}
