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

            var h = new House
            {
                Address = house.Address,
                MCName = house.MCName
            };

            _dbContext.Houses.Add(h);
            _dbContext.SaveChanges();
            return true;
        }
        public bool EditHouse(EditHouseDTO house)
        {
            House h = _dbContext.Houses.Find(house.Id);
            h.Address = house.Address;
            h.MCName = house.MCName;
            h.Rooms = house.Rooms;
            _dbContext.SaveChanges();
            return true;
        }

        public ReturnHouseDTO GetHouse(GetHouseInfoDTO h)
        {
            House house = _dbContext.Houses.Find(h.Id);

            return house.Map();
        }


        public async Task<IEnumerable<ReturnHouseDTO>> GetHouses()
            => await _dbContext.Houses.Select(house => house.Map()).ToListAsync();

        public bool RemoveHouse(RemoveHouseDTO h)
        {
            House house = _dbContext.Houses.Find(h.Id);
            _dbContext.Remove(house);
            _dbContext.SaveChanges();
            return true;
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
                .GroupBy(g => new { g.house.Id, g.house.Address, g.room.ApartamentNumber, g.meter.SerialNumber, g.meter.MeterData })
                .Select(s => new {
                    s.Key.Id,
                    s.Key.Address,
                    Value = s.Max(v => v.meter.MeterData)
                });

            var housesWithValue = _dbContext.Houses
                .Join(
                housesWithIndications,
                h => h.Id,
                hWI => hWI.Id,
                (h, hWI) => new { h.Id, hWI.Value })
                .GroupBy(o => o)
                .Select(h => new { h.Key.Id, Value = h.Sum(s => s.Value) });

            var houseWithMaxValue = await _dbContext.Houses
                .Join(
                    housesWithValue,
                    h => h.Id,
                    hWV => hWV.Id,
                    (h, hWV) => new { h.Id, hWV.Value })
                .GroupBy(o => o)
                .Select(h => new { h.Key.Id, Value = h.Max(m => m.Value) })
                .ToListAsync();
            var house = new GetHouseInfoDTO { Id = houseWithMaxValue.First().Id };
            return GetHouse(house);
             
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
                .GroupBy(g => new { g.house.Id, g.house.Address, g.room.ApartamentNumber, g.meter.SerialNumber, g.meter.MeterData })
                .Select(s => new {
                    s.Key.Id,
                    s.Key.Address,
                    Value = s.Max(v => v.meter.MeterData)
                });

            var housesWithValue = _dbContext.Houses
                .Join(
                housesWithIndications,
                h => h.Id,
                hWI => hWI.Id,
                (h, hWI) => new { h.Id, hWI.Value })
                .GroupBy(o => o)
                .Select(h => new { h.Key.Id, Value = h.Sum(s => s.Value) });

            var houseWithMinValue = await _dbContext.Houses
                .Join(
                    housesWithValue,
                    h => h.Id,
                    hWV => hWV.Id,
                    (h, hWV) => new { h.Id, hWV.Value })
                .GroupBy(o => o)
                .Select(h => new { h.Key.Id, Value = h.Min(m => m.Value) })
                .ToListAsync();
            var house = new GetHouseInfoDTO { Id = houseWithMinValue.First().Id };
            return GetHouse(house);
        }
        public IEnumerable<ReturnWaterMeterDTO> GetAllWaterMeters(GetHouseInfoDTO house)
        {
            //var roomsInHouse = _dbContext.Rooms.Where(r => r.HouseId == house.Id);
            //var waterMeters = roomsInHouse.SelectMany(r => _dbContext.WaterMeters.Where(w => w.RoomId == r.Id));

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
        public IEnumerable<Room> Rooms { get; set; }
    }
    public class ReturnWaterMeterDTO
    {
        public int Id { get; set; }
        public int WaterMeterData { get; set; }
    }
}
