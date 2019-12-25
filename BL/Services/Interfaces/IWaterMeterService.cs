using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Interfaces
{
    public interface IWaterMeterService
    {
        bool CreateWaterMeter(WaterMeterDTO waterMeter);
        bool InputDataWaterMeterId(InputDataWaterMeterIdDTO waterMeter);
        bool InputDataWaterMeterSerialNum(InputDataWaterMeterSerialNumDTO waterMeter);
        bool EditWaterMeter(WaterMeterDTO waterMeter);
        bool RemoveWaterMeter(int id);
        WaterMeterDTO GetWaterMeter(int id);
        
    }
}
