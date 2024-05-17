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
        private readonly object _lock = new object();

        public Consumer(ConveyerBelt inboundbelt)
        {
            InboundBelt = inboundbelt;
        }

        public void Pull(object obj)
        {
            while (true)
            {
                lock (_lock)
                {
                    CurrentLuggage = InboundBelt.Dequeue();
                }
            }
        }
    }
}
