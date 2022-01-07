using DataSavingApplication.Models;
using DataSavingApplication.Service.Interface;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace DataSavingApplication.Service
{
    public class AddDataService : IAddDataService
    {
        private readonly DataSavingRepo dataSavingRepo;
        public AddDataService()
        {
            dataSavingRepo = new DataSavingRepo();
        }

        public bool AddData(IFormFile file)
        {
            var list = new List<DataSavingModel>();

            FluentValidation validate = new FluentValidation();
            ValidationResult result = validate.Validate(file);

            if (!result.IsValid)
            {

                return false;
            }

            using (var stream = new MemoryStream())
            {
                file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    var rowcount = worksheet.Dimension.Rows;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        list.Add(new DataSavingModel
                        {
                            Name = worksheet.Cells[row, 1].Value.ToString().Trim(),
                            PhoneNumber = worksheet.Cells[row, 2].Value.ToString().Trim(),
                        });
                    }
                }
            }

            dataSavingRepo.AddData(list);

            return true;
        }
    }
}
