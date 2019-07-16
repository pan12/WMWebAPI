using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Interfaces
{
    public interface IHouseService
    {
        bool CreateHouse(CreateHouseDTO house);
        bool EditHouse(EditHouseDTO house);
        bool RemoveHouse(RemoveHouseDTO house);
        ReturnHouseDTO GetHouse(GetHouseInfoDTO house);
        Task<IEnumerable<ReturnHouseDTO>> GetHouses();
        Task<ReturnHouseDTO> GetHouseConsumptionMax();
        Task<ReturnHouseDTO> GetHouseConsumptionMin();
        IEnumerable<ReturnWaterMeterDTO> GetAllWaterMeters(GetHouseInfoDTO house);
    }

}
