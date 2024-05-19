using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    public class Luggage
    {
        public Luggage(int baggageID, int flightID)
        {
            this.baggageID = baggageID;
            this.FlightID = flightID;
        }

        public int baggageID { get; private set; }
        public int FlightID { get; private set; }
    }
}
