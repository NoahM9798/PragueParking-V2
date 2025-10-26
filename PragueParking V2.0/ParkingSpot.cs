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
        public string RegistrationNumber { get; set; }


        //Methods
        public void AddVehicle(Vehicle vehicle, int spotnumber)
        {
            VehiclesParked.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle, int spotnumber)
        {
            VehiclesParked.Remove(vehicle);
        }

        public bool CanVehicleFit(int spotnumber, Vehicle vehicle)
        {
            if (vehicle.Size <= AvailableSize)
            {
                //Vehicle can fit
                return true;
            }
            //Vehicle cannot fit
            return false;
        }

    }
}
