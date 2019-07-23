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
        public HouseService(Db dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateHouse(CreateHouseDTO house)
        {
            if (!_dbContext.Houses.Any(a => a.Address == house.Address))
            {
                var h = new House
                {
                    Address = house.Address,
                    MCName = house.MCName
                };

                _dbContext.Houses.Add(h);
                var result = _dbContext.SaveChanges();

                if (result > 0)
                    return true;
                else return false;
            }
            {
                return false;
            }
        }
        public bool EditHouse(EditHouseDTO house)
        {
            if (!_dbContext.Houses.Any(a => a.Address == house.Address))
            {
                try
                {
                    House h = _dbContext.Houses.Find(house.Id);
                    h.Address = house.Address;
                    h.MCName = house.MCName;
                    _dbContext.SaveChanges();
                    return true;

                }
                catch (NullReferenceException)
                {

                    return false;
                }
            }
            {
                return false;
            }
                
        }

        public ReturnHouseDTO GetHouse(GetHouseInfoDTO h)
        {
            try
            {
                return _dbContext.Houses.Find(h.Id).Map();
            }
            catch (NullReferenceException)
            {
                return null;
            }  
        }


        public async Task<IEnumerable<ReturnHouseDTO>> GetHouses()
            => await _dbContext.Houses.Select(house => house.Map()).ToListAsync();

        public bool RemoveHouse(RemoveHouseDTO h)
        {
            try
            {
                House house = _dbContext.Houses.Find(h.Id);
                _dbContext.Remove(house);
                _dbContext.SaveChanges();
                return true;
            }
            catch (NullReferenceException)
            {

                return false;
            }
        }

        public async Task<ReturnHouseDTO> GetHouseConsumptionMin()
        {
            var housesWithIndications = _dbContext.Houses
                .Join(
                    _dbContext.Rooms,
                    h => h.Id,
                    r => r.HouseId,
                    (h, r) => new { house = h, room = r }
                ).Join(
                    _dbContext.WaterMeters,
                    o => o.room.Id,
                    wm => wm.RoomId,
                    (o, wm) => new { o.house, o.room, meter = wm }
                )
                .GroupBy(g => new { g.house.Id, g.house.Address, g.house.MCName, g.meter.MeterData })
                .Select(s => new {
                    s.Key.Id,
                    s.Key.Address,
                    s.Key.MCName,
                    Value = s.Sum(v => v.meter.MeterData)
                });

            var min = housesWithIndications.Min(m => m.Value);
            var rh = await housesWithIndications.Where(m => m.Value == min).FirstAsync();
            ReturnHouseDTO returnHouse = new ReturnHouseDTO { Id = rh.Id, Address = rh.Address, MCName = rh.MCName };
            return (returnHouse);

        }

        public async Task<ReturnHouseDTO> GetHouseConsumptionMax()
        {
            var housesWithIndications = _dbContext.Houses
                .Join(
                    _dbContext.Rooms,
                    h => h.Id,
                    r => r.HouseId,
                    (h, r) => new { house = h, room = r }
                ).Join(
                    _dbContext.WaterMeters,
                    o => o.room.Id,
                    wm => wm.RoomId,
                    (o, wm) => new { o.house, o.room, meter = wm }
                )
                .GroupBy(g => new { g.house.Id, g.house.Address, g.house.MCName, g.meter.MeterData })
                .Select(s => new {
                    s.Key.Id,
                    s.Key.Address,
                    s.Key.MCName,
                    Value = s.Sum(v => v.meter.MeterData)
                });
            var max = housesWithIndications.Max(m => m.Value);
            var rh = await housesWithIndications.Where(m => m.Value == max).FirstAsync();
            ReturnHouseDTO returnHouse = new ReturnHouseDTO { Id = rh.Id, Address = rh.Address, MCName = rh.MCName };
            return (returnHouse);

        }
        public IEnumerable<ReturnWaterMeterDTO> GetAllWaterMeters(GetHouseInfoDTO house)
        {
            var waterMeters = _dbContext.Rooms
                .Where(r => r.HouseId == house.Id)
                .Join(
                    _dbContext.WaterMeters,
                    r => r.Id,
                    wm => wm.RoomId,
                    (r, wm) => wm
                )
                .ToList();

            return waterMeters.Select(w => w.Map());
        }
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

    }

    public class RemoveHouseDTO
    {
        public int Id { get; set; }
    }
    public class GetHouseInfoDTO
    {
        public int Id { get; set; }
    }
    public class EditHouseDTO
    {
        public int Id { get; set; }
        public string Address { get; set; }

        public string MCName { get; set; }
    }
    public class ReturnWaterMeterDTO
    {
        public int Id { get; set; }
        public int WaterMeterData { get; set; }
    }
}
