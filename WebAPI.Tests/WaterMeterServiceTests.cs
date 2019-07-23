using BL.Models;
using BL.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace WebAPI.Tests
{
    public class WaterMeterServiceTests
    {
        Db _dbContext;
        public WaterMeterServiceTests()
        {
            var options = new DbContextOptionsBuilder<Db>()
                .UseInMemoryDatabase(databaseName: "TestDB", databaseRoot: new InMemoryDatabaseRoot())
                .Options;

            _dbContext = new Db(options);

            _dbContext.AddRange(_testHouses);
            _dbContext.AddRange(_testRooms);
            _dbContext.AddRange(_testWaterMeters);
            _dbContext.SaveChanges();
        }
        List<House> _testHouses = new List<House>
        {
            new House {Id = 11, Address = "AAA", MCName = "a" },
            new House {Id = 12, Address = "BBB", MCName = "b" },
            new House {Id = 13, Address = "CCC", MCName = "c" },
        };
        List<Room> _testRooms = new List<Room>
        {
            new Room { Id = 11, ApartamentNumber = 12, HouseId = 11},
            new Room { Id = 12, ApartamentNumber = 1, HouseId = 11},
            new Room { Id = 13, ApartamentNumber = 2, HouseId = 11},
            new Room { Id = 14, ApartamentNumber = 12, HouseId = 12},
            new Room { Id = 15, ApartamentNumber = 20, HouseId = 13}
        };
        List<WaterMeter> _testWaterMeters = new List<WaterMeter>
        {
            new WaterMeter {Id = 11, MeterData = 123, RoomId = 11, SerialNumber = "wm1" },
            new WaterMeter {Id = 12, MeterData = 0, RoomId = 11, SerialNumber = "wm2" },
            new WaterMeter {Id = 13, MeterData = 7899, RoomId = 12, SerialNumber = "wm3" },
            new WaterMeter {Id = 14, MeterData = 256, RoomId = 13, SerialNumber = "wm4" },
            new WaterMeter {Id = 15, MeterData = 785, RoomId = 14, SerialNumber = "wm5" }
        };

        [Fact]
        public void CreateWaterMeter_NotNull()
        {
            CreateWaterMeterDTO waterMeterDTO = new CreateWaterMeterDTO
            {
                MeterData = 123,
                SerialNumber = "newWM",
                RoomId = 25
            };

            var service = new WaterMeterService(_dbContext);
            service.CreateWaterMeter(waterMeterDTO);

            var target = _dbContext.WaterMeters.FirstOrDefault(w 
                => w.MeterData == waterMeterDTO.MeterData &&
                w.SerialNumber == waterMeterDTO.SerialNumber);

            Assert.NotNull(target);
        }

        [Fact]
        public void CreateWaterMeter_SameID()
        {
            CreateWaterMeterDTO waterMeterDTO = new CreateWaterMeterDTO
            { SerialNumber = "wm1",
              RoomId = 97,
              MeterData = 156
            };
            
            var service = new WaterMeterService(_dbContext);
            service.CreateWaterMeter(waterMeterDTO);

            var target = _dbContext.WaterMeters.Where(wm 
                => wm.SerialNumber == waterMeterDTO.SerialNumber);

            Assert.Single(target);
        }

        [Fact]
        public void RegWaterMeter_NotNull()
        {
            RegWaterMeterDTO waterMeterDTO = new RegWaterMeterDTO
            {
                 Id = 15,
                 RoomId = 15
            };
            var service = new WaterMeterService(_dbContext);
            service.RegWaterMeter(waterMeterDTO);

            var target = _dbContext.WaterMeters.Where(w 
                => w.Id == waterMeterDTO.Id && w.RoomId == waterMeterDTO.RoomId);

            Assert.NotNull(target);
        }

        [Fact]
        public void InputDataWaterMeterId_NotEmpty()
        {
            InputDataWaterMeterIdDTO waterMeterDTO = new InputDataWaterMeterIdDTO
            {
                Id = 11,
                MeterData = 999
            };
            var service = new WaterMeterService(_dbContext);
            service.InputDataWaterMeterId(waterMeterDTO);

            var target = _dbContext.WaterMeters.Where(w
                => w.Id == waterMeterDTO.Id && w.MeterData == waterMeterDTO.MeterData);

            Assert.NotEmpty(target);

        }

        [Fact]
        public void InputDataWaterMeterId_Negative()
        {
            InputDataWaterMeterIdDTO waterMeterDTO = new InputDataWaterMeterIdDTO
            {
                Id = 11,
                MeterData = -999
            };
            var service = new WaterMeterService(_dbContext);
            service.InputDataWaterMeterId(waterMeterDTO);

            var target = _dbContext.WaterMeters.Where(w
                => w.Id == waterMeterDTO.Id && w.MeterData == waterMeterDTO.MeterData);

            Assert.Empty(target);

        }

        [Fact]
        public void InputDataWaterMeterSerialNum_NotEmpty()
        {
            InputDataWaterMeterSerialNumDTO waterMeterDTO = new InputDataWaterMeterSerialNumDTO
            {
                SerialNumber = "wm1",
                MeterData = 999
            };
            var service = new WaterMeterService(_dbContext);
            service.InputDataWaterMeterSerialNum(waterMeterDTO);

            var target = _dbContext.WaterMeters.Where(w
                => w.SerialNumber == waterMeterDTO.SerialNumber && w.MeterData == waterMeterDTO.MeterData);

            Assert.NotEmpty(target);

        }

        [Fact]
        public void InputDataWaterMeterSerialNum_Negative()
        {
            InputDataWaterMeterSerialNumDTO waterMeterDTO = new InputDataWaterMeterSerialNumDTO
            {
                SerialNumber = "wm1",
                MeterData = -999
            };
            var service = new WaterMeterService(_dbContext);
            service.InputDataWaterMeterSerialNum(waterMeterDTO);

            var target = _dbContext.WaterMeters.Where(w
                => w.SerialNumber == waterMeterDTO.SerialNumber && w.MeterData == waterMeterDTO.MeterData);

            Assert.Empty(target);

        }
    }
}
