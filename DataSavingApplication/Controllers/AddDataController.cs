using DataSavingApplication.Models;
using DataSavingApplication.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Controllers
{

    [ApiController]
    public class AddDataController : ControllerBase
    {
        private readonly IAddDataService _addDataService;
        public AddDataController(IAddDataService addDataService)
        {
            _addDataService = addDataService;
        }

        [Route("AddData")]
        [HttpPost]
        public IActionResult PostData([FromBody]List< DataSavingModel> requestData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var result = _addDataService.AddData(requestData);

                if (result == false)
                {
                    return BadRequest();
                }

                return Ok();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
            }

        }

        [Route("AddFile")]
        [HttpPost]
        public async Task<IActionResult> importExcel(IFormFile file)
        {
            var list = new List<DataSavingModel>();

            string extension = Path.GetExtension(file.FileName);

            if(extension != ".xlsx" || extension != ".xls")
            {
                return BadRequest();
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
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

            var result = _addDataService.AddData(list);

            return Ok(list);
        }
    }
}
