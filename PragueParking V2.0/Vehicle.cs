using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParking_V2._0
{
    public class Vehicle
    {
        public int Size { get; set; }
        public string RegNumber { get; set; }
        public DateTime ArrivalTime { get; set; } = DateTime.Now;
        public int PrizePerHour { get; set; }
        public string VehicleType { get; set; }

        //Constructor
        public Vehicle(string RegNumber)
        {
            this.RegNumber = RegNumber;
        }
    }
}
