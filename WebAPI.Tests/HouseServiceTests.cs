using BL.Models;
using BL.Services;
using BL.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WebAPI.Tests
{
    public class HouseServiceTests
    {
        Db _dbContext;
        public HouseServiceTests()
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
            new House {Id = 14, Address = "DDD", MCName = "d" },
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
            new WaterMeter {Id = 15, MeterData = 785, RoomId = 14, SerialNumber = "wm5" },
            new WaterMeter {Id = 16, MeterData = 785, RoomId = 15, SerialNumber = "wm6" }
        };
        [Fact]
        public void CreateHouse_NotNull()
        {
            CreateHouseDTO houseDTO = new CreateHouseDTO
            {
                Address = "123",
                MCName = "mcTest"
            };

            var service = new HouseService(_dbContext);
            service.CreateHouse(houseDTO);

            var target = _dbContext.Houses.FirstOrDefault(h => h.MCName == "mcTest" && h.Address == "123");

            Assert.NotNull(target);
        }
        
        [Fact]
        public void CreateHouse_SameAddress()
        {
            CreateHouseDTO houseDTO = new CreateHouseDTO { Address = "AAA", MCName = "mc1" };
            var service = new HouseService(_dbContext);
            service.CreateHouse(houseDTO);

            var target = _dbContext.Houses.Where(h => h.Address == houseDTO.Address);

            Assert.Single(target);
        }

        [Fact]
        public void EditHouse_NotNull()
        {
            EditHouseDTO editDTO = new EditHouseDTO { Id = 12, Address = "popo", MCName = "mc1" };
            var service = new HouseService(_dbContext);
            service.EditHouse(editDTO);

            var target = _dbContext.Houses
                .FirstOrDefault(
                h => h.Id == editDTO.Id &&
                h.Address == editDTO.Address &&
                h.MCName == editDTO.MCName);

            Assert.NotNull(target);
        }

        [Fact]
        public void EditHouse_SameAddress()
        {
            EditHouseDTO editDTO = new EditHouseDTO { Id = 11, Address = "AAA", MCName = "mc1" };
            var service = new HouseService(_dbContext);
            service.EditHouse(editDTO);

            var target = _dbContext.Houses
                .FirstOrDefault(
                h => h.Id == editDTO.Id &&
                h.Address == editDTO.Address && 
                h.MCName == editDTO.MCName);

            Assert.NotNull(target);
        }

        [Fact]
        public void EditHouse_BusyAddress()
        {
            EditHouseDTO editDTO = new EditHouseDTO { Id = 11, Address = "BBB", MCName = "mc1" };
            var service = new HouseService(_dbContext);
            service.EditHouse(editDTO);

            var target = _dbContext.Houses
                .FirstOrDefault(
                h => h.Id == editDTO.Id &&
                h.Address == editDTO.Address &&
                h.MCName == editDTO.MCName);

            Assert.Null(target);
        }


        [Fact]
        public void GetHouses_CountAsync()
        {

            var service = new HouseService(_dbContext);
            var a = service.GetHouses();
            Assert.Equal(4, a.Result.Count());
        }


        [Fact]
        public void GetHouses_Type()
        {
                var service = new HouseService(_dbContext);

                var a = service.GetHouses().Result as List<ReturnHouseDTO>;

                Assert.IsType<List<ReturnHouseDTO>>(a);
        }
        

        [Fact]
        public void RemoveHouse_FindNull()
        {
            RemoveHouseDTO removeDTO = new RemoveHouseDTO { Id = 11 };
            var service = new HouseService(_dbContext);
            service.RemoveHouse(removeDTO);

            var target = _dbContext.Houses.Find(removeDTO.Id);

            Assert.Null(target);
        }

        [Fact]
        public void GetAllWaterMeters_NotEmpty()
        {
            GetHouseInfoDTO h = new GetHouseInfoDTO { Id = 11 };
            var service = new HouseService(_dbContext);

            var target = service.GetAllWaterMeters(h);

            Assert.NotEmpty(target);
        }

        [Fact]
        public void GetAllWaterMeters_HaveAll()
        {
            GetHouseInfoDTO h = new GetHouseInfoDTO { Id = 11 };
            var service = new HouseService(_dbContext);

            var target = service.GetAllWaterMeters(h);

            Assert.Equal(4, target.Count());
        }

        [Fact]
        public void GetAllWaterMeters_Empty()
        {
            GetHouseInfoDTO h = new GetHouseInfoDTO { Id = 14 };
            var service = new HouseService(_dbContext);

            var target = service.GetAllWaterMeters(h);

            Assert.Empty(target);
        }

        [Fact]
        public void GetHouseConsumptionMax_NotNull()
        {
            var service = new HouseService(_dbContext);

            var target = service.GetHouseConsumptionMax();

            Assert.NotNull(target);
        }

        [Fact]
        public void GetHouseConsumptionMin_NotNull()
        {
            var service = new HouseService(_dbContext);

            var target = service.GetHouseConsumptionMin();

            Assert.NotNull(target);
        }

        [Fact]
        public void GetHouseConsumptionMax_True()
        {
            var service = new HouseService(_dbContext);

            var target = service.GetHouseConsumptionMax();

            Assert.True(target.Result.Id == 11);
        }

        [Fact]
        public void GetHouseConsumptionMin_True()
        {
            var service = new HouseService(_dbContext);

            var target = service.GetHouseConsumptionMin();

            Assert.True(target.Result.Id == 12 );
        }

        [Fact]
        public void GetHouse_NotNull()
        {
            GetHouseInfoDTO h = new GetHouseInfoDTO { Id = 11 };
            var service = new HouseService(_dbContext);

            var target = service.GetHouse(h);

            Assert.NotNull(target);
        }
        
        [Fact]
        public void GetHouse_Null()
        {
            GetHouseInfoDTO h = new GetHouseInfoDTO { Id = 45 };
            var service = new HouseService(_dbContext);

            var target = service.GetHouse(h);

            Assert.Null(target);
        }

        [Fact]
        public void GetHouse_Type()
        {
            GetHouseInfoDTO h = new GetHouseInfoDTO { Id = 11 };
            var service = new HouseService(_dbContext);

            var target = service.GetHouse(h);

            Assert.IsType<ReturnHouseDTO>(target);
        }


    }

}
