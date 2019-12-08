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
    public class RoomServiceTests
    {
        Db _dbContext;
        public RoomServiceTests()
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
        public void CreateRoom_NotNull()
        {
            CreateRoomDTO roomDTO = new CreateRoomDTO
            {
                 ApartamentNumber = 25
            };

            var service = new RoomService(_dbContext);
            service.CreateRoom(roomDTO);

            var target = _dbContext.Rooms.FirstOrDefault(r
                => r.ApartamentNumber == roomDTO.ApartamentNumber &&
                r.HouseId == roomDTO.HouseId);

            Assert.NotNull(target);
        }

        [Fact]
        public void RegWaterMeter_NotNull()
        {
            RegRoomDTO roomDTO = new RegRoomDTO
            {
                Id = 15,
                HouseId = 12
            };
            var service = new RoomService(_dbContext);
            service.RegRoom(roomDTO);

            var target = _dbContext.Rooms.Where(r
                => r.HouseId == roomDTO.HouseId && r.Id == roomDTO.Id);

            Assert.NotNull(target);
        }
    }
}
