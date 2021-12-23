using DataSavingApplication.Models;
using DataSavingApplication.Service.Interface;
using FluentValidation.Results;
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

        [Route("AddFile")]
        [HttpPost]
        public IActionResult importExcel(IFormFile file)
        {
            try
            {
                var data = _addDataService.AddData(file);

                if (data == false)
                {
                    return BadRequest();
                }

                return Ok();
            }

            catch(Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
            }

        }
    }
}
