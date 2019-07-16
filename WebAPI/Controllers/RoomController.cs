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
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        IRoomService _roomService;
        public RoomController (IRoomService roomService)
        {
            _roomService = roomService;
        }
        // GET: api/Room
        [HttpGet]
        
        // POST: api/Room
        [HttpPost]
        public bool PostRoom (CreateRoomDTO createRoom)
        {
            return _roomService.CreateRoom(createRoom);
        }

        // PUT: api/Room/5
        public bool PutRegRoom(RegRoomDTO room)
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
