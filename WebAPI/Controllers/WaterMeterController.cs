using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.Services;
using BL.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterMeterController : ControllerBase
    {
        IWaterMeterService _waterMeterService;
        public WaterMeterController(IWaterMeterService waterMeterService)
        {
            _waterMeterService = waterMeterService;
        }
        // GET: api/WaterMeter
        //[HttpGet]
        
        
        

        // POST: api/WaterMeter
        [HttpPost]
        public bool PostWaterMeter([FromBody] CreateWaterMeterDBO waterMeter)
        {
            return _waterMeterService.CreateWaterMeter(waterMeter);
        }

        // PUT: api/WaterMeter/5
        [HttpPut]

        public bool PutDataWaterMeterId([FromBody] InputDataWaterMeterIdDBO waterMeter)
        {
            return _waterMeterService.InputDataWaterMeterId(waterMeter);
        }
        public bool PutDataWaterMeterSerialNum([FromBody] InputDataWaterMeterSerialNumDBO waterMeter)
        {
            return _waterMeterService.InputDataWaterMeterSerialNum(waterMeter);
        }
        public bool PutRegWaterMeter(RegWaterMeterDBO waterMeter)
        {
            return _waterMeterService.RegWaterMeter(waterMeter);
        }

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        
    }
}
