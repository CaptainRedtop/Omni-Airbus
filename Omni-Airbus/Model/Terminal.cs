using Omni_Airbus.Controller;
using Omni_Airbus.Model.Booking;
using Omni_Airbus.Utils;
using Omni_Airbus.Utils.Logging;

namespace Omni_Airbus.Model
{
	public class Terminal : Consumer
	{
		public ConveyerBelt InboundBelt;
		public Luggage? CurrentLuggage;
		private Logger log;
		List<CheckIn> CheckIns = new List<CheckIn>();
		public bool Gate;
		private readonly object obj = new object();
		int LuggageCount = 0;
		private Aircraft? CurrentAircraft;
		public bool open = false;
		public int ID;

		/// <summary>
		/// Initializes a new instance of the Terminal class.
		/// </summary>
		/// <param name="inboundbelt">The conveyer belt handling incoming luggage.</param>
		/// <param name="CheckIn1">First check-in record.</param>
		/// <param name="CheckIn2">Second check-in record.</param>
		/// <param name="id">Unique identifier for this terminal.</param>
		public Terminal(ConveyerBelt inboundbelt, CheckIn CheckIn1, CheckIn CheckIn2, int id) : base(inboundbelt)
		{
			CheckIns.Add(CheckIn1);
			CheckIns.Add(CheckIn2);
			InboundBelt = inboundbelt;
			log = new Logger(0);
			ID = id;
		}

		#region Pull
		/// <summary>
		/// Continuously attempts to load luggage onto aircraft until the terminal is closed.
		/// </summary>
		/// <param name="obj">An object passed to the method but not used in this implementation.</param>
		public override void Pull(object obj)
		{
			while (true)
			{
				SecureFlight();
				TryRenewAircraft();

				if (open)
				{
					if (LuggageCount < CurrentAircraft.TotalSeats)
					{
						CurrentLuggage = InboundBelt.Dequeue();
						if (CurrentLuggage != null)
						{
							log.Information($"Loading Luggage: {CurrentLuggage.baggageID}");
						}
					}
				}
				Thread.Sleep(0.01f.ToMilliseconds());
			}
		}

		/// <summary>
		/// Attempts to renew the aircraft if the current one is null or full.
		/// </summary>
		private void TryRenewAircraft()
		{
			if (CurrentAircraft == null)
			{
				GetNewAircraft();
				foreach (CheckIn checkIn in CheckIns)
				{
					if (checkIn.CurrentFlight == null)
					{
						checkIn.CurrentFlight = CurrentFlight;
					}
				}
				SetCheckIn(true);
			}
			else if (LuggageCount >= CurrentAircraft.TotalSeats)
			{
				SetCheckIn(false);
				UpdateCurrentFlight();
				GetNewAircraft();
				SetCheckIn(true);
				Report.Aircrafts.Add(CurrentFlight.AircraftId);
				Report.Desinations.Add(CurrentFlight.DestinationId);
				Report.Airlines.Add(CurrentFlight.AirlineId);
			}
		}

		/// <summary>
		/// Updates the current flight based on the state of the terminal and check-ins.
		/// </summary>
		private void UpdateCurrentFlight()
		{
			if (CurrentFlight == null)
			{
				SetCurrentFlight();
				foreach (CheckIn checkIn in CheckIns)
				{
					if (checkIn.CurrentFlight == null)
					{
						checkIn.CurrentFlight = CurrentFlight;
						break;
					}
					else if (checkIn.CurrentFlight == CurrentFlight)
					{
						checkIn.CurrentFlight = CurrentFlight;
					}
				}
			}
			else
			{
				foreach (CheckIn checkIn in CheckIns)
				{
					if (checkIn.CurrentFlight == CurrentFlight)
					{
						SetCurrentFlight();
						checkIn.CurrentFlight = CurrentFlight;
					}
				}
			}
		}

		/// <summary>
		/// Sets the current flight for the terminal and associated check-ins.
		/// </summary>
		private void SetCurrentFlight()
		{
			lock (LuggageSystem.Flights)
			{
				CurrentFlight = LuggageSystem.Flights.Dequeue();
			}
		}

		/// <summary>
		/// Retrieves a new aircraft for loading luggage.
		/// </summary>
		private void GetNewAircraft()
		{
			if (CurrentAircraft != null)
			{
				log.Information($"Aircraft: {CurrentAircraft.AircraftId} is full");
			}
			CurrentAircraft = new Aircraft(CurrentFlight.AircraftId);
			log.Information($"New Aircraft: {CurrentFlight.AircraftId}");
		}

		/// <summary>
		/// Opens or closes the terminal based on the check-in status.
		/// </summary>
		/// <param name="value">True to open, False to close the terminal.</param>
		private void SetCheckIn(bool value)
		{
			open = value;
			foreach (CheckIn checkIn in CheckIns)
			{
				bool FlightCheckIn = checkIn.CurrentFlight != null;
				bool FlightTerminal = CurrentFlight != null;
				bool FlightMatch = checkIn.CurrentFlight.FlightId == CurrentFlight.FlightId;
				if (FlightCheckIn && FlightTerminal && FlightMatch)
				{
					checkIn.open = value;
					log.Information($"Set CheckIn: {checkIn.ID} To: {value}");
				}
			}
		}

		/// <summary>
		/// Secures a flight for processing if none is currently assigned.
		/// </summary>
		private void SecureFlight()
		{
			if (CurrentFlight == null)
			{
				lock (LuggageSystem.Flights)
				{
					CurrentFlight = LuggageSystem.Flights.Dequeue();
				}
			}
		}
		#endregion
	}
}
