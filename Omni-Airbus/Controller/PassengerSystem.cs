using Omni_Airbus.Model;
using Omni_Airbus.Model.Booking;

namespace Omni_Airbus.Controller
{
	/// <summary>
	/// This is the PassengerSystem which is controller.
	/// </summary>
	internal class PassengerSystem
	{
		private ReservationsSystem _reservationsSystem;

		public PassengerSystem(ReservationsSystem reservationsSystem)
		{
			_reservationsSystem = reservationsSystem;
		}

		/// <summary>
		/// Add a new passenger.
		/// </summary>
		/// <param name="passenger"></param>
		public void NewPassenger(Passenger passenger)
		{
			_reservationsSystem.Passengers.Add(passenger);
		}
		/// <summary>
		/// Remove a passenger.
		/// </summary>
		/// <param name="passengerId"></param>
		public void RemovePassenger(int passengerId)
		{
			foreach (var passenger in _reservationsSystem.Passengers)
			{
				if (passenger.PassengerId == passengerId)
				{
					_reservationsSystem.Passengers.Remove(passenger);
					break;  // Exits the loop once the passenger has been found and REMOVED
				}
			}
		}
		/// <summary>
		/// Edits a passenger.
		/// </summary>
		/// <param name="passengerId"></param>
		/// <param name="firstName"></param>
		/// <param name="lastName"></param>
		/// <param name="passportNumber"></param>
		/// <param name="flightId"></param>
		/// <param name="boardingPassNumber"></param>
		public void EditPassenger(int passengerId, string firstName, string lastName, string passportNumber, int flightId, string boardingPassNumber)
		{
			foreach (var passenger in _reservationsSystem.Passengers)
			{
				if (passenger.PassengerId == passengerId)
				{
					passenger.FirstName = firstName;
					passenger.LastName = lastName;
					passenger.PassportNumber = passportNumber;
					passenger.FlightId = flightId;
					passenger.BoardingPassNumber = boardingPassNumber;
					break;  // Exits the loop-de-loop once the passenger has been found and EDITED
				}
			}
		}
	}
}
