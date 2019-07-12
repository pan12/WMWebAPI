using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.Models;
using BL.Services.Interfaces;

namespace BL.Services
{
    public class WaterMeterService : IWaterMeterService
    {
        Db _dbContext;
        public WaterMeterService(Db dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateWaterMeter(CreateWaterMeterDBO waterMeter)
        {
            var vM = new WaterMeter
            {
                RoomId = waterMeter.RoomId,
                SerialNumber = waterMeter.SerialNumber,
                MeterData = waterMeter.MeterData
            };
            _dbContext.WaterMeters.Add(vM);
            _dbContext.SaveChanges();
            return true;
        }
        public bool RegWaterMeter(RegWaterMeterDBO waterMeter)
        {
            _dbContext.WaterMeters.Find(waterMeter.Id).RoomId = waterMeter.RoomId;
            _dbContext.SaveChanges();
            return true;
        }
        public bool InputDataWaterMeterId(InputDataWaterMeterIdDBO waterMeter)
        {
            _dbContext.WaterMeters.Find(waterMeter.Id).MeterData = waterMeter.MeterData;
            _dbContext.SaveChanges();
            return true;
            
        }
        public bool InputDataWaterMeterSerialNum(InputDataWaterMeterSerialNumDBO waterMeter)
        {
            _dbContext.WaterMeters.Single(WaterMeter => WaterMeter.SerialNumber == waterMeter.SerialNumber).MeterData
                = waterMeter.MeterData;
            _dbContext.SaveChanges();
            return true;
        }
    }
    public class CreateWaterMeterDBO
    {

        public int RoomId { get; set; }
        public string SerialNumber { get; set; }
        public int MeterData { get; set; }
    }
    public class RegWaterMeterDBO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
    }
    public class InputDataWaterMeterIdDBO
    {
        public int Id { get; set; }
        public int MeterData { get; set; }
    }
    public class InputDataWaterMeterSerialNumDBO
    {
        public string SerialNumber { get; set; }
        public int MeterData { get; set; }
    }

}
