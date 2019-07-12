using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Interfaces
{
    public interface IWaterMeterService
    {
        bool CreateWaterMeter(CreateWaterMeterDBO waterMeter);
        bool RegWaterMeter(RegWaterMeterDBO waterMeter);
        bool InputDataWaterMeterId(InputDataWaterMeterIdDBO waterMeter);
        bool InputDataWaterMeterSerialNum(InputDataWaterMeterSerialNumDBO waterMeter);

    }
}
