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

        public static GarageConfig Config;
        public ParkingGarage()
        {
            Config = ConfigManager.LoadConfig();
            InitializeSpots();
        }

        //Methods

        //Initialize parking spots based on json file, if its null create new spots
        private void InitializeSpots()
        {
            Data.LoadData(this);

            // If no saved data, create fresh parking spots
            if (ParkingSpots == null || ParkingSpots.Count == 0)
            {
                for (int i = 1; i <= Config.GarageSize; i++)
                {
                    ParkingSpots.Add(new ParkingSpot
                    {
                        SpotNumber = i,
                        AvailableSize = Config.SpotSize
                    });
                }
            }
        }
        //Helpful methods to retrieve information

        public Vehicle getVehicleByReg(string reg)
        {
            foreach(var spot in ParkingSpots)
            {
                foreach (var vehicle in spot.VehiclesParked)
                {
                    if (vehicle.RegNumber == reg)
                    {
                        return vehicle;
                    }
                }
            }
            return null;
        }
        public int FinalPrice(Vehicle vehicle, DateTime timeRetrieved)
        {
            TimeSpan TimeParked = timeRetrieved - vehicle.ArrivalTime;
            int totalMinutes = (int)TimeParked.TotalMinutes;
            if (totalMinutes <= 10)
            {
                //First 10 minutes are free
                return 0;
            }
            else
            {
                int hoursParked = (int)Math.Ceiling((totalMinutes - 10) / 60.0);
                return hoursParked * vehicle.PrizePerHour;
            }
        }

        //Methods that fills a function
        public bool ParkVehicle(Vehicle vehicle, int spotnumber = -1)
        {
            if (spotnumber != -1)
            {
                //A spot number has been passed in, user wants to move vehicle to specific spot
                foreach (var spot in ParkingSpots)
                {
                    if (spot.SpotNumber == spotnumber)
                    {
                        //Heres the place the vehicle wants to be moved to
                        if (spot.CanVehicleFit(spot.SpotNumber, vehicle))
                        {
                            //Success!
                            //First lets add it into new spot
                            spot.AddVehicle(vehicle, spotnumber);
                            spot.AvailableSize -= vehicle.Size;
                            //Then we need to remove it from its old spot
                            TryRemoveVehicle(vehicle);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
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

        public bool TryRemoveVehicle(Vehicle vehicle)
        {
            foreach (var spot in ParkingSpots)
            {
                foreach (Vehicle v in spot.VehiclesParked)
                {
                    if (v == vehicle)
                    {
                        //Found the vehicle, now remove it
                        spot.RemoveVehicle(vehicle, spot.SpotNumber);
                        spot.AvailableSize += vehicle.Size;
                        return true;
                    }
                }
            }
            return false;
        }

        

        
    }
}
