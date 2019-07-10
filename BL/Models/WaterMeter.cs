using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Models
{
    public class WaterMeter
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }        
        public string SerialNumber { get; set; }
        public int MeterReading { get; set; }
    }
}
