using BL.Models;
using BL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Services
{
    public class RoomService : IRoomService
    {
        Db _dbContext;
        public RoomService(Db dbContext)
        {
            _dbContext = dbContext;
        }
        public bool CreateRoom (RoomDTO room)
        {
            if (!HouseExists(room.HouseId))
                return false;
            var r = new Room { ApartamentNumber = room.ApartamentNumber, HouseId = room.HouseId };
            _dbContext.Rooms.Add(r);
            _dbContext.SaveChanges();
            return true;

        }
        public bool EditRoom(RoomDTO room)
        {
            if (!HouseExists(room.HouseId))
                return false;
            var r = new Room { ApartamentNumber = room.ApartamentNumber, HouseId = room.HouseId };
            _dbContext.Rooms.Add(r);
            _dbContext.SaveChanges();
            return true;

        }
        public RoomDTO GetRoom(int id)
        {
            return _dbContext.Rooms.Find(id).Map();
        }
        public bool RemoveRoom(int id)
        {
            var room =_dbContext.Rooms.Find(id);
            _dbContext.WaterMeters.RemoveRange(
                _dbContext.WaterMeters.Where(wm => wm.RoomId == id));
            _dbContext.SaveChanges();
            return true;
        }
        bool HouseExists(int houseId)
        {
            return _dbContext.Houses.Find(houseId) != null;
        }
    }
}
