using Omni_Airbus.Model.Booking;
using Omni_Airbus.Utils;

namespace Omni_Airbus.Model
{
    public abstract class Consumer
    {
        protected Luggage? CurrentLuggage;
        public ConveyerBelt? InboundBelt;
        private readonly object _lock = new object();
        public Flight CurrentFlight;

        public Consumer()
        {
        }
        public Consumer(ConveyerBelt inboundbelt)
        {
            InboundBelt = inboundbelt;
        }


        public virtual void Pull(object obj)
        {
            while (true)
            {
                lock (_lock)
                {
                    CurrentLuggage = InboundBelt.Dequeue();
                }
                Thread.Sleep(0.01f.ToMilliseconds());
            }
        }
    }
}
