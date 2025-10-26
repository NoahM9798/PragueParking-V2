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
        private GarageConfig _config = new GarageConfig();

        //Constructor
        public MC(string RegNumber) : base(RegNumber)
        {
            Size = _config.MCSize;
            PrizePerHour = _config.PricePerMC;
        }
    }
}
