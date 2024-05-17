using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    public abstract class Consumer
    {
        protected Luggage CurrentLuggage;
        protected ConveyerBelt InboundBelt;

        public Consumer(ConveyerBelt inboundbelt)
        {
            InboundBelt = inboundbelt;
        }

        public void Pull(object obj)
        {

        }
    }
}
