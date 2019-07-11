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
        public void CreateHouse(CreateHouseDTO house)
        {

            var h = new House
            {
                Address = house.Address,
                MCName = house.MCName
            };

            _dbContext.Houses.Add(h);
            _dbContext.SaveChanges();
        }

        public ReturnHouseDTO GetHouseInfo(int id)
        {
            House house = _dbContext.Houses.Find(id);

            return house.Map();
        }

        //public async Task<IEnumerable<ReturnHouseDTO>> GetHouses()
        //{
        //    IEnumerable<House>  houses = await _dbContext.Houses.ToListAsync();

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
        public string Address { get; set; }
        
    }
}
