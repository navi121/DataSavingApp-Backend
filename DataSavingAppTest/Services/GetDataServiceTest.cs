using AutoFixture;
using DataSavingApplication.Models;
using DataSavingApplication.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DataSavingAppTest.Services
{
    public class GetDataServiceTest
    {
        private readonly DataSavingRepo _dataSavingRepo;
        private readonly GetDataService _getDataService;
        private readonly Fixture _fixture = new Fixture();
        public GetDataServiceTest()
        {
            _dataSavingRepo = new DataSavingRepo();
            _getDataService = new GetDataService();
        }
        [Fact]
        public async Task Get_All_Data_From_Db()
        {
            //Arrange
            var expectedResult = _dataSavingRepo.GetAllData().ToList();

            //Act
            var result = _getDataService.GetDatas().Result.ToList();

            //Assert
            Assert.Equal(expectedResult.Count,result.Count);
        }
    }
}
