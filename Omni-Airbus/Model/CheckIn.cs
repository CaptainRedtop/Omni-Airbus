using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    internal class CheckIn
    {
        public bool Registered;
        private int ID = 0;
        public ConveyerBelt OutboundBelt;

        public CheckIn(ConveyerBelt inboundbelt)
        {
            OutboundBelt = inboundbelt;
        }

        public void Checked()
        {
            Luggage luggage = new Luggage();
            ID++;
            luggage.baggageID = ID;
            OutboundBelt.Enqueue(luggage);
        }
    }
}
