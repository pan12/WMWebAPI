using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Models;
using BL.Services;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        IHouseService _houseService;
        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }

        
        [HttpGet]
        [Route("houses")]
        public Task<IEnumerable<HouseDTO>> GetHouses()
        {
            return _houseService.GetHouses();
        }

        [Route("house/consumptionMax")]
        public Task<HouseDTO>
            GetConsumptionMax()
        {
            return _houseService.GetHouseConsumptionMax();
        }

        [Route("house/consumptionMin")]
        public Task<HouseDTO> GetConsumptionMin()
        {
            return _houseService.GetHouseConsumptionMin();
        }

        [Route("house/{id}")]
        public HouseDTO GetHouse(int id)
        {
            return _houseService.GetHouse(id);
        }

        [Route("house/{id}/meters")]
        public IEnumerable<WaterMeterDTO> GetAllWaterMeters(int id)
        {
            return _houseService.GetAllWaterMeters(id);
        }

        // POST: api/House
        [HttpPost]
        [Route("house")]
        public bool PostHouse ([FromBody] HouseDTO houseDTO)
        {
            return _houseService.CreateHouse(houseDTO);
        }

        // PUT: api/House/5
        [HttpPut]
        [Route("house")]
        public bool PutHouse([FromBody] HouseDTO houseDTO)
        {
            return _houseService.EditHouse(houseDTO);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("house/{id}")]
        public bool DeleteHouse (int id)
        {
            return _houseService.RemoveHouse(id);
        }
    }
}
