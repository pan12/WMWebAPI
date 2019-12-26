using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.Services;
using BL.Services.Interfaces;
using BL.Models;

namespace WebAPI.Controllers
{
    [Route("api/wMeter")]
    [ApiController]
    public class WaterMeterController : ControllerBase
    {
        IWaterMeterService _waterMeterService;
        public WaterMeterController(IWaterMeterService waterMeterService)
        {
            _waterMeterService = waterMeterService;
        }

        [HttpPost]
        public bool PostWaterMeter([FromBody] WaterMeterDTO waterMeter)
        {
            return _waterMeterService.CreateWaterMeter(waterMeter);
        }


        [HttpPut]
        [Route("inputDataID")]
        public bool PutDataWaterMeterId([FromBody] InputDataWaterMeterIdDTO waterMeter)
        {
            return _waterMeterService.InputDataWaterMeterId(waterMeter);
        }

        [Route("inputDataSerNum")]
        public bool PutDataWaterMeterSerialNum([FromBody] InputDataWaterMeterSerialNumDTO waterMeter)
        {
            return _waterMeterService.InputDataWaterMeterSerialNum(waterMeter);
        }

        public bool EditWaterMeter([FromBody] WaterMeterDTO waterMeter)
        {
            return _waterMeterService.EditWaterMeter(waterMeter);
        }

        [HttpDelete]
        [Route("{id}")]
        public bool RemoveWaterMeter(int id)
        {
            return _waterMeterService.RemoveWaterMeter(id);
        }
        [HttpGet]
        [Route("{id}")]
        public WaterMeterDTO GetWaterMeter (int id)
        {
            return _waterMeterService.GetWaterMeter(id);
        }
    }
}
