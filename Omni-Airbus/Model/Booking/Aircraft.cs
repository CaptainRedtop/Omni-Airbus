using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model.Booking
{
	/// <summary>
	/// This gets the aircraft information with an ID, model, and total seats.
	/// </summary>
	internal class Aircraft
	{
		private int _aircraftId; 
		private string _model; 
		private int _totalSeats; 

		public int AircraftId
		{
			get { return _aircraftId; }
			set { _aircraftId = value; }
		}

		public string Model
		{
			get { return _model; }
			set { _model = value; }
		}

		public int TotalSeats
		{
			get { return _totalSeats; }
			set { _totalSeats = value; }
		}
	}
}
