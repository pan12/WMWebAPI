using BL.Models;
using BL.Services.Interfaces;
using System;
using System.Collections.Generic;
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
        public bool CreateRoom (CreateRoomDTO room)
        {
            var r = new Room { ApartamentNumber = room.ApartamentNumber, HouseId = room.HouseId };
            _dbContext.Rooms.Add(r);
            _dbContext.SaveChanges();
            return true;

        }
        public bool RegRoom (RegRoomDTO room)
        {
            var r = _dbContext.Rooms.Find(room.Id);
            r.HouseId = room.HouseId;
            _dbContext.SaveChanges();
            
            return true;
        }
    }
    public class CreateRoomDTO
    {
        public int ApartamentNumber { get; set;}
        public int HouseId { get; set; }

    }
    public class RegRoomDTO
    {
        public int Id { get; set; }
        public int HouseId { get; set; }

    }
}
