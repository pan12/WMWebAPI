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
    [Produces("application/json")]
    [Route("api")]
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
        [Route("room")]
        public bool PostRoom ([FromBody]RoomDTO createRoom)
        {
            return _roomService.CreateRoom(createRoom);
        }
    }
}
