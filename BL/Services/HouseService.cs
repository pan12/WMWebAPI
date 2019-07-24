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
                ).Where(w => w.Values.Any());
            var houseSumValue = housesWithIndications.Select(s => new {
                s.house,
                Value = s.Values.Sum(sum=> sum.waterMeter)
            });
            var min = houseSumValue.Min(m => m.Value);
            var rh =  houseSumValue.FirstOrDefault(f => f.Value == min);
            return rh.house.Map();

        }

        public async Task<ReturnHouseDTO> GetHouseConsumptionMax()
        {
            var housesWithIndications = _dbContext.Houses
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
                );
            var houseSumValue = housesWithIndications.Select(s => new {
                s.house,
                Value = s.Values.Sum(sum => sum.waterMeter)
            });
            var max = houseSumValue.Max(m => m.Value);
            return houseSumValue.FirstOrDefault(f => f.Value == max).house.Map();

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
