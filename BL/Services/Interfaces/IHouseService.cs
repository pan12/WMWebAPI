using System;
using System.Collections.Generic;
using System.Text;
using BL.Models;

namespace BL.Services.Interfaces
{
    public interface IHouseService
    {
        bool CreateHouse(CreateHouseDTO house);
        bool RemoveHouse(RemoveHouseDTO id);
        ReturnHouseDTO GetHouse(GetHouseInfoDTO id);
    }

}
