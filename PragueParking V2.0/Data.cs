using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using ConfigPragueParking;

namespace PragueParking_V2._0
{
    public class Data
    {
        private static readonly string dataPath = "../../../../garageData.json";
        public static GarageConfig Config = ConfigManager.LoadConfig();

        public static void SaveData(ParkingGarage garage)
        {
            string json = JsonConvert.SerializeObject(garage.ParkingSpots, Formatting.Indented);
            ConfigManager.SaveGarage(dataPath, json);
        }

        public static void LoadData(ParkingGarage garage)
        {
            if (!File.Exists(dataPath))
                return; 

            string json = ConfigManager.LoadGarage(dataPath);
            var parkingSpots = JsonConvert.DeserializeObject<List<ParkingSpot>>(json);

            if (parkingSpots != null && parkingSpots.Count > 0)
            {
                garage.ParkingSpots = parkingSpots;
            }
        }

        //This method checks old data with new data, and sees if we need to create new garage with new config
        //Values, or if we can keep the old one
        public static bool canHaveGarageOpenDuringRenovation(ParkingGarage garage)
        {
            //First, see if user changed spot size or vehicle sizes, if so we need to send out new garage
            foreach (ParkingSpot spot in garage.ParkingSpots)
            {
                if (spot.VehiclesParked == null)
                {
                    //Found empty spot, here we can compare available size with config spot size
                    if (spot.AvailableSize != Data.Config.SpotSize)
                    {
                        return false;
                    }
                }
                else
                {
                    //This spot cointains vehicles, we need to check each vehicle size
                    foreach (Vehicle vehicle in spot.VehiclesParked)
                    {
                        if (vehicle.VehicleType == "MC" && vehicle.Size != Config.MCSize)
                        {
                            return false;
                        }
                        else if(vehicle.VehicleType == "Car" && vehicle.Size != Config.CarSize)
                        {
                            return false;
                        }
                    }
                }
            }
            //If we made it here, we need to check the garage size
            if (garage.ParkingSpots.Count > Data.Config.GarageSize)
            {
                //Garage size decreased, we need to see if there are vehicles in the spots that will be removed
                for (int i = Data.Config.GarageSize; i < garage.ParkingSpots.Count; i++)
                {
                    if (garage.ParkingSpots[i].VehiclesParked != null && garage.ParkingSpots[i].VehiclesParked.Count > 0)
                    {
                        //Found vehicles in the spots that will be removed, cannot keep garage open
                        return false;
                    }
                }

            }
            else if (garage.ParkingSpots.Count < Data.Config.GarageSize)
            {
                //Garage size increased, this is fine we can keep vehicles inside just need to add new spots
                return true;
            }
            return true;
        }
    }
}