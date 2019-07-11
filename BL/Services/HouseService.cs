using BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BL.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BL.Services
{
    public class HouseService : IHouseService
    {
        Db _dbContext;
        public HouseService (Db dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateHouse(CreateHouseDTO house)
        {

            var h = new House
            {
                Address = house.Address,
                MCName = house.MCName
            };

            _dbContext.Houses.Add(h);
            _dbContext.SaveChanges();
            return true;
        }

        public ReturnHouseDTO GetHouse(GetHouseInfoDTO h)
        {
            House house = _dbContext.Houses.Find(h.Id);

            return house.Map();
        }

        public IEnumerable<ReturnHouseDTO> GetHouses()
        {
            var houses = _dbContext.Houses;
            List<ReturnHouseDTO> returnHouses = new List<ReturnHouseDTO>();

            foreach (var b in houses)
            {
                returnHouses.Add(b.Map());
            }

            return returnHouses;
        }

        public async Task<IEnumerable<ReturnHouseDTO>> GetHousesAsync()
            => await _dbContext.Houses.Select(house => house.Map()).ToListAsync();

        public bool RemoveHouse (RemoveHouseDTO h)
        {            
            House house = _dbContext.Houses.Find(h.Id);
            _dbContext.Remove(house);
            _dbContext.SaveChanges();
            return true;
        }

        //public async Task<IEnumerable<ReturnHouseDTO>> GetHouses()
        //{
        //    IEnumerable<House> houses = await _dbContext.Houses.ToListAsync();

        //    return houses.Select(house => house.MapToReturnModel);
        //}
    }


    public class CreateHouseDTO
    {
        public string Address { get; set; }
        public string MCName { get; set; }
    }

    public class ReturnHouseDTO
    {

        public int Id { get; set; }
        public string Address { get; set; }

        public string MCName { get; set; }
        public IEnumerable<Room> Rooms { get; set; }

    }

    public class RemoveHouseDTO
    {
        public int Id { get; set; }
    }
    public class GetHouseInfoDTO
    {
        public int Id { get; set; }
    }

}
