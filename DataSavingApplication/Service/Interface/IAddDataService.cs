using DataSavingApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Service.Interface
{
    public interface IAddDataService
    {
        public bool AddData(List<DataSavingModel> requestData);
    }
}
