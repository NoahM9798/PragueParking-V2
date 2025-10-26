using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConfigPragueParking;

namespace PragueParking_V2._0
{
    public class Car : Vehicle
    {
        private GarageConfig _config = new GarageConfig();

        //Constructor
        public Car(string RegNumber) : base(RegNumber)
        {
            Size = _config.CarSize;
            PrizePerHour = _config.PricePerCar;
            VehicleType = "Car";
        }
    }
}
