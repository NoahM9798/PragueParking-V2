using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PragueParking_V2._0
{
    public class MC : Vehicle
    {

        //Constructor
        public MC(string RegNumber) : base(RegNumber)
        {
            Size = 1;
            PrizePerHour = 30;
        }
    }
}
