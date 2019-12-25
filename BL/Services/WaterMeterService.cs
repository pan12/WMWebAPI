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


        
        public bool CreateWaterMeter(WaterMeterDTO waterMeter)
        {
            
            if (waterMeter.MeterData >= 0 
                && !SerialNumberExists(waterMeter.SerialNumber)
                && RoomExists(waterMeter.RoomId))
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
            { 
                return false; 
            }
        }
        public bool InputDataWaterMeterId(InputDataWaterMeterIdDTO waterMeter)
        {
            if (waterMeter.MeterData < 0)
                return false;
            var wM = _dbContext.WaterMeters.Find(waterMeter.Id);
            if (wM == null)
                return false;
            wM.MeterData = waterMeter.MeterData;
            _dbContext.SaveChanges();
            return true;
        }
        public bool InputDataWaterMeterSerialNum(InputDataWaterMeterSerialNumDTO waterMeter)
        {
            if (waterMeter.MeterData < 0)
                return false;
            var wM = _dbContext.WaterMeters.SingleOrDefault(wMDB => wMDB.SerialNumber == waterMeter.SerialNumber);
            if (wM == null)
                return false;
            wM.MeterData = waterMeter.MeterData;
            _dbContext.SaveChanges();
            return true;
        }
        public bool EditWaterMeter(WaterMeterDTO waterMeter)
        {
            if (waterMeter.MeterData >= 0
                && !SerialNumberExists(waterMeter.SerialNumber)
                && RoomExists(waterMeter.RoomId))
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
            {
                return false;
            }
        }
        public bool RemoveWaterMeter(int wmId)
        {
            var wm = _dbContext.WaterMeters.Find(wmId);
            if (wm == null)
                return false;
            _dbContext.WaterMeters.Remove(wm);
            _dbContext.SaveChanges();
            return true;

        }
        public WaterMeterDTO GetWaterMeter(int wmId)
        {
            return _dbContext.WaterMeters.Find(wmId).Map();
        }

        bool SerialNumberExists(string serialNumber)
        {
            return _dbContext.WaterMeters.Any(a => a.SerialNumber == serialNumber);
        }
        bool RoomExists(int roomId)
        {
            return (_dbContext.Rooms.Find(roomId) != null);
        }
    }

}
