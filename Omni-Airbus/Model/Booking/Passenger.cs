using System;

namespace Omni_Airbus.Model.Booking
{
	/// <summary>
	/// Passenger with an ID, first name, last name, passport number, flight ID, and boarding pass number.
	/// </summary>
	internal class Passenger
	{
		private int _passengerId;
		private string _firstName;
		private string _lastName; 
		private string _passportNumber; 
		private int _flightId; 
		private string _boardingPassNumber; 

		public int PassengerId
		{
			get { return _passengerId; }
			set { _passengerId = value; }
		}

		public string FirstName
		{
			get { return _firstName; }
			set { _firstName = value; }
		}

		public string LastName
		{
			get { return _lastName; }
			set { _lastName = value; }
		}

		public string PassportNumber
		{
			get { return _passportNumber; }
			set { _passportNumber = value; }
		}

		public int FlightId
		{
			get { return _flightId; }
			set { _flightId = value; }
		}

		public string BoardingPassNumber
		{
			get { return _boardingPassNumber; }
			set { _boardingPassNumber = value; }
		}
	}
}
