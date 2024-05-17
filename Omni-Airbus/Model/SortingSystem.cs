using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    public class SortingSystem
    {
        public int BaggageID;
        public DateTime InboundTime;
        public DateTime OutboundTime;
        private Queue<Luggage> InboundConveyorBelt;
        private Queue<Luggage> OutboundConveyorBelt;

        public SortingSystem()
        {
            InboundConveyorBelt = new Queue<Luggage>();
            OutboundConveyorBelt = new Queue<Luggage>();
        }

        private void SendToGate()
        {

        }
    }
}
