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
        public bool CreateHouse(HouseDTO house)
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
        public bool EditHouse(HouseDTO house)
        {
            if (_dbContext.Houses.Any(a => (a.Address == house.Address && a.Id != house.Id)))
                return false;
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

        public HouseDTO GetHouse(int houseId)
        {
            try
            {
                return _dbContext.Houses.Find(houseId).Map();
            }
            catch (NullReferenceException)
            {
                return null;
            }  
        }


        public async Task<IEnumerable<HouseDTO>> GetHouses()
            => await _dbContext.Houses.Select(house => house.Map()).ToListAsync();

        public bool RemoveHouse(int houseId)
        {
            var house = _dbContext.Houses.Find(houseId);
            if (house == null)
                return false;
            _dbContext.Houses.Remove(house);
            var rooms =  _dbContext.Rooms.Where(r => r.HouseId == houseId);
            var wms = _dbContext.WaterMeters.Where(wm => rooms.Any(r => r.Id == wm.RoomId));
            _dbContext.Rooms.RemoveRange(rooms);
            _dbContext.WaterMeters.RemoveRange(wms);
            _dbContext.SaveChanges();
            return true;
        }

        public async Task<HouseDTO> GetHouseConsumptionMin()
        {
            var houseSumValue = _dbContext.Houses
                .GroupJoin(
                    _dbContext.Rooms,
                    h => h.Id,
                    r => r.HouseId,
                    (h, r) => new
                    {
                        house = h,
                        Values = r.GroupJoin(
                            _dbContext.WaterMeters,
                            room => room.Id,
                            wm => wm.RoomId,
                            (room, wm) => new { waterMeter = wm.Sum(s => s.MeterData) }
                        ) }
                ).Where(w => w.Values.Any()).Select(s => new {
                    s.house,
                    Value = s.Values.Sum(sum => sum.waterMeter)
                });
            var min = await  houseSumValue.MinAsync(m => m.Value);
            return houseSumValue.FirstOrDefault(f => f.Value == min).house.Map();

        }

        public async Task<HouseDTO> GetHouseConsumptionMax()
        {
            var houseSumValue = _dbContext.Houses
                .GroupJoin(
                    _dbContext.Rooms,
                    h => h.Id,
                    r => r.HouseId,
                    (h, r) => new
                    {
                        house = h,
                        Values = r.GroupJoin(
                            _dbContext.WaterMeters,
                            room => room.Id,
                            wm => wm.RoomId,
                            (room, wm) => new { waterMeter = wm.Sum(s => s.MeterData) }
                        )
                    }
                ).Select(s => new {
                    s.house,
                    Value = s.Values.Sum(sum => sum.waterMeter)
                });
            var max = await houseSumValue.MaxAsync(m => m.Value);
            return houseSumValue.FirstOrDefault(f => f.Value == max).house.Map();

        }
        public IEnumerable<WaterMeterDTO> GetAllWaterMeters(int houseId)
        {
            var waterMeters = _dbContext.Rooms
                .Where(r => r.HouseId == houseId)
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
}
