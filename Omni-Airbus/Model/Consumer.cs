using Omni_Airbus.Model.Booking;
using Omni_Airbus.Utils;

namespace Omni_Airbus.Model
{
	/// <summary>
	/// Represents a consumer in the Omni Airbus model.
	/// </summary>
	public abstract class Consumer
    {
        protected Luggage? CurrentLuggage;
        public ConveyerBelt? InboundBelt;
        private readonly object _lock = new object();
        public Flight CurrentFlight;

        public Consumer()
        {
        }

		/// <summary>
		/// The inbound conveyer belt from which the consumer pulls luggage.
		/// </summary>
		public Consumer(ConveyerBelt inboundbelt)
        {
            InboundBelt = inboundbelt;
        }

		/// <summary>
		/// Pulls luggage from the inbound conveyer belt.
		/// </summary>
		/// <param name="obj">The object to pull.</param>
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
