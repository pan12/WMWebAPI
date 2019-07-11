using System;
using System.Collections.Generic;
using System.Text;
using BL.Models;

namespace BL.Services.Interfaces
{
    public interface IHouseService
    {
        void CreateHouse(CreateHouseDTO house);
    }

}
