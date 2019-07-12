using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Interfaces
{
    public interface IHouseService
    {
        bool CreateHouse(CreateHouseDTO house);
        bool EditHouse(EditHouseDTO house);
        bool RemoveHouse(RemoveHouseDTO house);
        ReturnHouseDTO GetHouse(GetHouseInfoDTO house);
        //ReturnHouseDTO GetHouseConsumptionMax();
        //ReturnHouseDTO GetHouseConsumptionMin();
    }

}
