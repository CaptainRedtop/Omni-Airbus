using Omni_Airbus.Utils.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    public class Terminal : Consumer
    {
        public ConveyerBelt InboundBelt;
        public Luggage CurrentLuggage;
        private Logger log;
        public bool Gate;
        private readonly object obj = new object();

        public Terminal(ConveyerBelt inboundbelt) : base(inboundbelt)
        {
            InboundBelt = inboundbelt;
            log = new Logger(0);
        }

        public void Pull(object obj)
        {
            while (true)
            {
                lock (obj)
                {
                    CurrentLuggage = InboundBelt.Dequeue();
                    log.Information($"{CurrentLuggage.baggageID}");
                }
            }

        }

    }
}
