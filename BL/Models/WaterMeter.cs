using BL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string SerialNumber { get; set; }
        public int MeterData { get; set; }

        public WaterMeterDTO Map()
            => new WaterMeterDTO 
            {Id = this.Id, RoomId = this.RoomId, SerialNumber = this.SerialNumber, MeterData = this.MeterData };

    }
    public class InputDataWaterMeterIdDTO
    {
        public int Id { get; set; }
        public int MeterData { get; set; }
    }
    public class InputDataWaterMeterSerialNumDTO
    {
        public string SerialNumber { get; set; }
        public int MeterData { get; set; }
    }
    public class WaterMeterDTO
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string SerialNumber { get; set; }
        public int MeterData { get; set; }
    }
}
