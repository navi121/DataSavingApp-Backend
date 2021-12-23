using DataSavingApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataSavingApplication.Controllers
{

    [ApiController]
    public class GetDataController : ControllerBase
    {
        private readonly DataSavingRepo dataSavingRepo;
        public GetDataController()
        {
            dataSavingRepo = new DataSavingRepo();
        }

        [Route("GetData")]
        [HttpGet]
        public IActionResult GetAllData()
        {
            try
            {
                var data = dataSavingRepo.GetAllData();

                if (data == null)
                {
                    return NoContent();
                }

                return Ok(data);
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
            }
        }
    }
}
