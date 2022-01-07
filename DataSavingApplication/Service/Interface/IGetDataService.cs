using DataSavingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Service.Interface
{
    public interface IGetDataService
    {
        public Task<IEnumerable<DataSavingModel>> GetDatas();
    }
}
