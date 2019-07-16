﻿using BL.Services.Interfaces;
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
            int max = 0;
            House houseMax = _dbContext.Houses.First();
            foreach (var house in _dbContext.Houses)
            {
                var meterDataSum = await _dbContext.Houses
                    .Select(h => h.Id)
                    .Join(
                        _dbContext.Rooms,
                        h => h,
                        r => r.HouseId,
                        (h, r) => r
                    ).Join(
                        _dbContext.WaterMeters,
                        r => r.Id,
                        wm => wm.RoomId,
                        (r, wm) => wm
                    ).SumAsync(wm => wm.MeterData);
                if (meterDataSum > max) { max = meterDataSum; houseMax = house; };
            }
            return houseMax.Map();
                
        }
        public async Task<ReturnHouseDTO> GetHouseConsumptionMin()
        {
            //var min = _dbContext.WaterMeters.Aggregate((h1, h2) => h1.MeterData < h2.MeterData ? h1 : h2);
            //var room = await _dbContext.Rooms.FindAsync(min.RoomId);
            //var house = await _dbContext.Houses.FindAsync(room.HouseId);
            
            House houseMin = _dbContext.Houses.First();
            int min = 999999999;
            foreach (var house in _dbContext.Houses)
            {
                var meterDataSum = await _dbContext.Houses
                    .Select(h => h.Id)
                    .Join(
                        _dbContext.Rooms,
                        h => h,
                        r => r.HouseId,
                        (h, r) => r
                    ).Join(
                        _dbContext.WaterMeters,
                        r => r.Id,
                        wm => wm.RoomId,
                        (r, wm) => wm
                    ).SumAsync(wm => wm.MeterData);
                if (meterDataSum < min)  { min = meterDataSum; houseMin = house; };
            }
            return houseMin.Map();
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
