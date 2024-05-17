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
        private Queue<Luggage> InboundConveyorBelt;
        private Queue<Luggage> OutboundConveyorBelt;

        // Help with the Time Properties

        public SortingSystem()
        {
            InboundConveyorBelt = new Queue<Luggage>();
            OutboundConveyorBelt = new Queue<Luggage>();
        }

        public void PopulateInboundConveyorBelts()
        {

        }
        public void PopulateOutboundConveyorBelts()
        {

        }
    }
}
