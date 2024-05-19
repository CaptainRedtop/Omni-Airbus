using Omni_Airbus.Model.Booking;
using Omni_Airbus.Utils;
using Omni_Airbus.Utils.Logging;

namespace Omni_Airbus.Model
{
        /// <summary>
    /// Represents a check-in station in the airport luggage system.
    /// This class handles the registration of luggage and its transfer to outbound conveyer belts.
    /// </summary>
    public class CheckIn
    {
        public bool Registered;
        public int ID;
        private static int luggageID;
        public ConveyerBelt OutboundBelt;
        public Flight CurrentFlight;
        public bool open;
        private Logger Log;
        private static object luggageIDLock = new object();

        public CheckIn(ConveyerBelt inboundbelt, int id)
        {
            Log = new Logger(LoggerEnum.Information);
            OutboundBelt = inboundbelt;
            ID = id;
            luggageID = 0;
        }


		/// <summary>
		/// Continuously checks in luggage until the check-in station is closed.
		/// </summary>
		/// <param name="obj">Unused parameter required by the delegate signature.</param>
		public void Checked(object obj)
        {
            while (true)
            {
                if(Thread.CurrentThread.Name == null)
                {
                    Thread.CurrentThread.Name = $"Thread: CheckIn{ID}";
                }
                if (open && OutboundBelt.Count < ConveyerBelt.MAX_SIZE)
                {
                    int currentLuggageID;
                    lock (luggageIDLock) // Locking this block to make it thread-safe
                    {
                        currentLuggageID = luggageID;
                        luggageID++;
                    }
                    Luggage luggage = new Luggage(currentLuggageID, CurrentFlight.AircraftId);
                    luggageID++;
                    OutboundBelt.Enqueue(luggage);
                    Log.Information($"[{Thread.CurrentThread.Name}] " +
                        $"  Luggage {luggage.baggageID} " +
                        $"  checked into flight: {CurrentFlight.FlightId} " +
                        $"  with Aircraft ID: {CurrentFlight.AircraftId} " +
                        $"  Loaded unto belt: {OutboundBelt.ID}");
                }
                Thread.Sleep(0.01f.ToMilliseconds());
            }
        }

		/// <summary>
		/// Retrieves the total number of luggage checked into the system.
		/// </summary>
		/// <returns>The total number of luggage checked in.</returns>
		public static int GetLuggageCheckedIn()
        {
            return luggageID;
        }
    }
}