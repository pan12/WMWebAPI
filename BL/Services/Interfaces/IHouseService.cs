using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IHouseService
    {
        bool CreateHouse(HouseDTO house);
        bool EditHouse(HouseDTO house);
        bool RemoveHouse(int houseID);
        HouseDTO GetHouse(int houseId);
        Task<IEnumerable<HouseDTO>> GetHouses();
        Task<HouseDTO> GetHouseConsumptionMax();
        Task<HouseDTO> GetHouseConsumptionMin();
        IEnumerable<WaterMeterDTO> GetAllWaterMeters(int houseId);
    }

}
