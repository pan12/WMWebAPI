using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.Services;
using BL.Services.Interfaces;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        IRoomService _roomService;
        public RoomController (IRoomService roomService)
        {
            _roomService = roomService;
        }        
        // POST: api/Room
        [HttpPost]
        public bool PostRoom ([FromBody]CreateRoomDTO createRoom)
        {
            return _roomService.CreateRoom(createRoom);
        }

        // PUT: api/Room/5
        public bool PutRegRoom([FromBody]RegRoomDTO room)
        {
            return _roomService.RegRoom(room);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
