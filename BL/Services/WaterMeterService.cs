using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BL.Models;
using BL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            if ((waterMeter.MeterData >= 0) 
                && (!_dbContext.WaterMeters.Any(a => a.SerialNumber == waterMeter.SerialNumber)))
            {
                var wM = new WaterMeter
                {
                    RoomId = waterMeter.RoomId,
                    SerialNumber = waterMeter.SerialNumber,
                    MeterData = waterMeter.MeterData
                };
                _dbContext.WaterMeters.Add(wM);
                _dbContext.SaveChanges();
                return true;
            }
            { return false; }
        }
        public bool RegWaterMeter(RegWaterMeterDBO waterMeter)
        {
            var wM = _dbContext.WaterMeters.Find(waterMeter.Id);
            wM.RoomId = waterMeter.RoomId;
            _dbContext.SaveChanges();
            return true;
        }
        public bool InputDataWaterMeterId(InputDataWaterMeterIdDBO waterMeter)
        {
            if (waterMeter.MeterData >=0)
            {
                var wM = _dbContext.WaterMeters.Find(waterMeter.Id);
                wM.MeterData = waterMeter.MeterData;
                _dbContext.SaveChanges();
                return true;

            }
            { return false; }
            
        }
        public bool InputDataWaterMeterSerialNum(InputDataWaterMeterSerialNumDBO waterMeter)
        {
            if (waterMeter.MeterData >= 0)
            {
                var wM = _dbContext.WaterMeters.SingleOrDefault(wMDB => wMDB.SerialNumber == waterMeter.SerialNumber);
                wM.MeterData = waterMeter.MeterData;
                _dbContext.SaveChanges();
                return true;
            }
            { return false; }
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
