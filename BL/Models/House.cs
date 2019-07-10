using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BL.Models
{
    public class House
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string MCName { get; set; }
        public IEnumerable<Room> Rooms { get; set; }

        public House()
        {
            Rooms = new HashSet<Room>();
        }
    }
}
