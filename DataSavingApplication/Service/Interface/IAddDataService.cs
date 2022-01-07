using DataSavingApplication.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Service.Interface
{
    public interface IAddDataService
    {
        public bool AddData(IFormFile file);
    }
}
