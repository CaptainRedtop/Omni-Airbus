using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model.Booking
{
	/// <summary>
	/// The available seats for a flight with a flight ID and available seats.
	/// </summary>
	internal class AvailableSeat
	{
		private int _flightId;
		private int _availableSeats;

		public int FlightId
		{
			get { return _flightId; }
			set { _flightId = value; }
		}

		public int AvailableSeats
		{
			get { return _availableSeats; }
			set { _availableSeats = value; }
		}
	}
}

