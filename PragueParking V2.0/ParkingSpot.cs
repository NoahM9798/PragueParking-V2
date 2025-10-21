using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PragueParking_V2._0
{
    public class ParkingSpot
    {
        //variables
        public List<Vehicle> VehiclesParked { get; set; } = new List<Vehicle>();
        public int AvailableSize { get; set; }
        public int SpotNumber { get; set; }

        //Constructor
        public ParkingSpot(int spotNumber)
        {
            //Load following data from JSON in the future
            
        }

        //Methods
        public void AddVehicle(Vehicle vehicle)
        {
            VehiclesParked.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            VehiclesParked.Remove(vehicle);
        }


    }
}
