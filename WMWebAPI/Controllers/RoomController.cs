using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BL.Services;
using BL.Services.Interfaces;
using BL.Models;

namespace WebAPI.Controllers
{
    [Route("api/room")]
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
        public bool PostRoom ([FromBody]RoomDTO createRoom)
        {
            return _roomService.CreateRoom(createRoom);
        }
        [HttpPut]
        public bool EditRoom([FromBody] RoomDTO room)
        {
            return _roomService.EditRoom(room);
        }

        [HttpDelete]
        [Route("{id}")]
        public bool RemoveRoom(int id)
        {
            return _roomService.RemoveRoom(id);
        }
        [HttpGet]
        [Route("{id}")]
        public RoomDTO GetRoom(int id)
        {
            return _roomService.GetRoom(id);
        }

    }
}
