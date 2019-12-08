using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Services.Interfaces
{
    public interface IRoomService
    {
        bool CreateRoom(CreateRoomDTO room);
        bool RegRoom(RegRoomDTO room);
    }
}
