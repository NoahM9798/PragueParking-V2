using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ConfigPragueParking;

namespace PragueParking_V2._0
{
    public class MC : Vehicle
    {
        //Constructor
        public MC(string RegNumber) : base(RegNumber)
        {
            Size = Data.Config.MCSize;
            PrizePerHour = Data.Config.PricePerMC;
            VehicleType = "MC";
        }
    }
}
