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
        //Constructor
        public Car(string RegNumber) : base(RegNumber)
        {
            Size = Data.Config.CarSize;
            PrizePerHour = Data.Config.PricePerCar;
            VehicleType = "Car";
        }
    }
}
