namespace Omni_Airbus.Model
{
	/// <summary>
	/// Represents a piece of luggage in the Omni Airbus model.
	/// </summary>
	public class Luggage
	{
		/// <summary>
		/// Initializes a new instance of the Luggage class with a specified baggage ID and flight ID.
		/// </summary>
		/// <param name="baggageID">The ID of the baggage.</param>
		/// <param name="flightID">The ID of the flight.</param>
		public Luggage(int baggageID, int flightID)
		{
			this.baggageID = baggageID;
			this.FlightID = flightID;
		}

		/// <summary>
		/// Gets the ID of the baggage.
		/// </summary>
		public int baggageID { get; private set; }

		/// <summary>
		/// Gets the ID of the flight.
		/// </summary>
		public int FlightID { get; private set; }
	}
}
