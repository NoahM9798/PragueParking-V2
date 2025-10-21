using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParking_V2._0
{
    public class Car : Vehicle
    {

        //Constructor
        public Car(string RegNumber) : base(RegNumber)
        {
            Size = 3;
            PrizePerHour = 60; //TODO: Get from JSON
            VehicleType = "Car";
        }
    }
}
