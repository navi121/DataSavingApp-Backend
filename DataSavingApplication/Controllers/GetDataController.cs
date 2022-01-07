using DataSavingApplication.Models;
using DataSavingApplication.Service.Interface;
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
        private readonly IGetDataService _getDataService;
        public GetDataController(IGetDataService getDataService)
        {
            _getDataService = getDataService;
        }

        [Route("GetData")]
        [HttpGet]
        public IActionResult GetAllData()
        {
            try
            {
                var data = _getDataService.GetDatas();

                if (data == null)
                {
                    return NoContent();
                }

                return Ok(data.Result);
            }

            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { ex.Message, Type = ex.GetType().ToString() });
            }
        }
    }
}
