using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        IHouseService _houseService;
        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
        }
        // GET: api/House
        [HttpGet]
        public Task<IEnumerable<ReturnHouseDTO>> GetHouses()
        {
            return _houseService.GetHouses();
        }
        public Task<ReturnHouseDTO> GetConsumptionMax()
        {
            return _houseService.GetHouseConsumptionMax();
        }
        public Task<ReturnHouseDTO> GetConsumptionMin()
        {
            return _houseService.GetHouseConsumptionMin();
        }

        // GET: api/House/5

        [HttpGet("{id}")]
        public ReturnHouseDTO GetHouse(int id)
        {
            GetHouseInfoDTO infoDTO = new GetHouseInfoDTO { Id = id };
            return _houseService.GetHouse(infoDTO);
        }
        IEnumerable<ReturnWaterMeterDTO> GetAllWaterMeters(int id)
        {
            GetHouseInfoDTO infoDTO = new GetHouseInfoDTO { Id = id };
            return _houseService.GetAllWaterMeters(infoDTO);
        }


        // POST: api/House
        [HttpPost]
        public bool PostHouse ([FromBody] CreateHouseDTO houseDTO)
        {

            return _houseService.CreateHouse(houseDTO);
        }

        // PUT: api/House/5
        [HttpPut]
        public bool PutHouse([FromBody] EditHouseDTO houseDTO)
        {

            return _houseService.EditHouse(houseDTO);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public bool DeleteHouse (int id)
        {
            RemoveHouseDTO remove = new RemoveHouseDTO { Id = id };
            return _houseService.RemoveHouse(remove);
        }
    }
}
