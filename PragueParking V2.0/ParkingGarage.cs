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
            Data.LoadData(this);

            // If no saved data, create fresh parking spots
            if (ParkingSpots == null || ParkingSpots.Count == 0)
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
        }
        public bool ParkVehicle(Vehicle vehicle)
        {
            foreach (var spot in ParkingSpots)
            {
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

        public bool VehicleExists(string regNumber)
        {
            foreach (var spot in ParkingSpots)
            {
                foreach (var vehicle in spot.VehiclesParked)
                {
                    if (vehicle.RegNumber == regNumber)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
