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
        private ConveyerBelt InboundBelt;

        public CheckIn(ConveyerBelt inboundbelt)
        {
            InboundBelt = inboundbelt;
        }

        public void Checked(Luggage luggage)
        {
            ID++;
            luggage.baggageID = ID;
            InboundBelt.Enqueue(luggage);
        }

    }
}
