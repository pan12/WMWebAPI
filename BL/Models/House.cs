using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using System.ComponentModel.DataAnnotations;

namespace BL.Models
{
    public class House
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        public string MCName { get; set; }        
        public IEnumerable<Room> Rooms { get; set; }

        public House()
        {
            Rooms = new HashSet<Room>();
        }


        public ReturnHouseDTO Map()
            => new ReturnHouseDTO { Id = this.Id, Address = this.Address, MCName = this.MCName};
        
    }
}
