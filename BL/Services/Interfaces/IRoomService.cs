using BL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Interfaces
{
    public interface IRoomService
    {
        bool CreateRoom(RoomDTO room);
        bool EditRoom(RoomDTO room);
        bool RemoveRoom(int roomId);
        RoomDTO GetRoom(int roomId);
    }
}
