using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class WaterMeter
    {
        private int Id {get; }
        private int HomeId { get; set; }
        private string FactoryNumber { get; set; }
        private int MeterReading { get; set; }
        private int CurrId = 0;

        public WaterMeter()
        {
            Id = CurrId++;
        }
    }
}
