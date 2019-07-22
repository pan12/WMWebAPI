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

            _dbContext.AddRange(testHouses);
            _dbContext.SaveChanges();
        }

        List<House> testHouses = new List<House>
        {
            new House {Id = 10, Address = "AAA", MCName = "a" },
            new House {Id = 20, Address = "BBB", MCName = "b" },
            new House {Id = 30, Address = "CCC", MCName = "c" },
        };

        List<ReturnHouseDTO> returnHouses = new List<ReturnHouseDTO>
        {
            new ReturnHouseDTO { Id = 10, Address = "AAA", MCName = "a"},
            new ReturnHouseDTO { Id = 20, Address = "BBB", MCName = "b"},
            new ReturnHouseDTO { Id = 30, Address = "CCC", MCName = "c"}
        };

        [Fact]
        public void CreateHouse_NotNull()
        {
            CreateHouseDTO houseDTO = new CreateHouseDTO
            {
                Address = "123",
                MCName = "fasdfs"
            };

            var service = new HouseService(_dbContext);
            service.CreateHouse(houseDTO);

            var target = _dbContext.Houses.FirstOrDefault(h => h.MCName == "fasdfs" && h.Address == "123");

            Assert.NotNull(target);
        }

        [Fact]
        public void CreateHouse_SameAddress()
        {
            CreateHouseDTO houseDTO = new CreateHouseDTO { Address = "qwe", MCName = "mc1" };
            CreateHouseDTO houseDTO2 = new CreateHouseDTO { Address = "qwe", MCName = "mc2" };
            var service = new HouseService(_dbContext);
            service.CreateHouse(houseDTO);
            service.CreateHouse(houseDTO2);

            var target = _dbContext.Houses.FirstOrDefault(h => h.MCName == "mc2" && h.Address == "qwe");

            Assert.Null(target);
        }

        [Fact]
        public void EditHouse_NotNull()
        {
            EditHouseDTO editDTO = new EditHouseDTO { Id = 10, Address = "popo", MCName = "mc1" };
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
            EditHouseDTO editDTO = new EditHouseDTO { Id = 10, Address = "AAA", MCName = "mc1" };
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
        public void GetHouses_Count()
        {
            
                var service = new HouseService(_dbContext);
                var a = service.GetHouses().Result as List<ReturnHouseDTO>;
                Assert.Equal(3, a.Count);
        }

        [Fact]
        public void GetHouses_Type()
        {
                var service = new HouseService(_dbContext);

                var a = service.GetHouses().Result as List<ReturnHouseDTO>;

                Assert.IsType<List<ReturnHouseDTO>>(a);
        }

        [Fact]
        public void GetHouses_Type2()
        {
            var data = new List<House>
            {
                new House {Id = 10, Address = "AAA", MCName = "a" },
                new House {Id = 20, Address = "BBB", MCName = "b" },
                new House {Id = 30, Address = "CCC", MCName = "c" }
            }.AsQueryable();
            var mockSet = new Mock<DbSet<House>>();
            mockSet.As<IQueryable<House>>().Setup(s => s.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<House>>().Setup(s => s.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<House>>().Setup(s => s.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<House>>().Setup(s => s.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<Db>();
            mockContext.Setup(m => m.Houses).Returns(mockSet.Object);

            var service = new HouseService(mockContext.Object);
            

            var result = service.GetHouses();
            //mock.Setup(service => service.Houses.AddRange(testHouses));
            //var hs = new HouseService(mock.Object);

            //var result = hs.GetHouses();


            //Assert.IsType<Task<IEnumerable<ReturnHouseDTO>>>(result);
            Assert.Equal(3, result.Result.Count());
        }


        [Fact]
        public void RemoveHouse_FindNull()
        {
            RemoveHouseDTO removeDTO = new RemoveHouseDTO { Id = 10 };
            var service = new HouseService(_dbContext);
            service.RemoveHouse(removeDTO);

            var target = _dbContext.Houses.Find(removeDTO.Id);

            Assert.Null(target);
        }
    }

}
