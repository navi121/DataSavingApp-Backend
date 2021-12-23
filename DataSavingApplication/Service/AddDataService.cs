using DataSavingApplication.Models;
using DataSavingApplication.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Service
{
    public class AddDataService : IAddDataService
    {
        private readonly DataSavingRepo dataSavingRepo;
        public AddDataService()
        {
            dataSavingRepo = new DataSavingRepo();
        }

        public bool AddData(List<DataSavingModel> requestData)
        {
            //var data = requestData.Cast<object>().ToList();
            dataSavingRepo.AddData(requestData);

            return true;
        }
    }
}
