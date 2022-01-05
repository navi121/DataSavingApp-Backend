using DataSavingApplication.Models;
using DataSavingApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Service
{
    public class GetDataService : IGetDataService
    {
        private readonly DataSavingRepo dataSavingRepo;

        public GetDataService()
        {
            dataSavingRepo = new DataSavingRepo();
        }

        public async Task<IEnumerable<DataSavingModel>> GetDatas()
        {
            var data = dataSavingRepo.GetAllData();

            return data;
        }
    }
}
