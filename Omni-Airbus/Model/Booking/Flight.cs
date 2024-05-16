using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace Omni_Airbus.Model.Booking
{
	/// <summary>
	/// "Flight" with ID, flight number, departure time, arrival time, airline ID, aircraft ID, and destination ID.
	/// </summary>
	internal class Flight
	{
		private int _flightId; 
		private string _flightNumber; 
		private DateTime _departureTime;
		private DateTime _arrivalTime; 
		private int _airlineId;
		private int _aircraftId;
		private int _destinationId;

		public int FlightId
		{
			get { return _flightId; }
			set { _flightId = value; }
		}

		public string FlightNumber
		{
			get { return _flightNumber; }
			set { _flightNumber = value; }
		}

		public DateTime DepartureTime
		{
			get { return _departureTime; }
			set { _departureTime = value; }
		}

		public DateTime ArrivalTime
		{
			get { return _arrivalTime; }
			set { _arrivalTime = value; }
		}

		public int AirlineId
		{
			get { return _airlineId; }
			set { _airlineId = value; }
		}

		public int AircraftId
		{
			get { return _aircraftId; }
			set { _aircraftId = value; }
		}

		public int DestinationId
		{
			get { return _destinationId; }
			set { _destinationId = value; }
		}
	}
}
