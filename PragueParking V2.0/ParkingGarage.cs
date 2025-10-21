using ConfigPragueParking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PragueParking_V2._0;
using Spectre.Console;

namespace PragueParking_V2._0
{
    public class ParkingGarage
    {
        //A list of all the parking spots
        public List<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
        private GarageConfig _config;
        public ParkingGarage()
        {
            _config = ConfigManager.LoadConfig();
            InitializeSpots();
        }

        //Methods
        private void InitializeSpots()
        {
            for (int i = 1; i <= _config.GarageSize; i++)
            {
                ParkingSpots.Add(new ParkingSpot
                {
                    SpotNumber = i,
                    AvailableSize = _config.SpotSize
                });
            }
        }
        public bool ParkVehicle(Vehicle vehicle)
        {
            foreach (var spot in ParkingSpots)
            {
                Console.WriteLine(spot.CanVehicleFit + " " + spot.AvailableSize);
                if (spot.CanVehicleFit(spot.SpotNumber, vehicle))
                {
                    //We found a spot in which the vehicle can fit, so we park it there
                    spot.AddVehicle(vehicle, spot.SpotNumber);
                    spot.AvailableSize -= vehicle.Size;
                    return true;
                }
            }
            //No available spot found
            return false;
        }
    }
}
