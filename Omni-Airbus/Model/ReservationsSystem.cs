using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Omni_Airbus.Model.Booking;

namespace Omni_Airbus.Model
{
	/// <summary>
	/// This is a reservation system for managing airlines, aircrafts,
	/// destinations, flights, passengers, and available seats.
	/// </summary>
	internal class ReservationsSystem
	{
		private List<Airline> _airlines;
		private List<Aircraft> _aircrafts; 
		private List<Destination> _destinations; 
		private List<Flight> _flights; 
		private List<Passenger> _passengers; 
		private List<AvailableSeat> _availableSeats; 

		public List<Airline> Airlines
		{
			get { return _airlines; }
			set { _airlines = value; }
		}

		public List<Aircraft> Aircrafts
		{
			get { return _aircrafts; }
			set { _aircrafts = value; }
		}

		public List<Destination> Destinations
		{
			get { return _destinations; }
			set { _destinations = value; }
		}

		public List<Flight> Flights
		{
			get { return _flights; }
			set { _flights = value; }
		}

		public List<Passenger> Passengers
		{
			get { return _passengers; }
			set { _passengers = value; }
		}

		public List<AvailableSeat> AvailableSeats
		{
			get { return _availableSeats; }
			set { _availableSeats = value; }
		}

		public ReservationsSystem()
		{
			Airlines = new List<Airline>(); 
			Aircrafts = new List<Aircraft>(); 
			Destinations = new List<Destination>(); 
			Flights = new List<Flight>(); 
			Passengers = new List<Passenger>(); 
			AvailableSeats = new List<AvailableSeat>(); 
		}
	}
}
