using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Interfaces
{
    public interface IWaterMeterService
    {
        bool CreateWaterMeter(CreateWaterMeterDTO waterMeter);
        bool RegWaterMeter(RegWaterMeterDTO waterMeter);
        bool InputDataWaterMeterId(InputDataWaterMeterIdDTO waterMeter);
        bool InputDataWaterMeterSerialNum(InputDataWaterMeterSerialNumDTO waterMeter);

    }
}
