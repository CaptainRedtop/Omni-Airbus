using Omni_Airbus.Utils;
using Omni_Airbus.Utils.Logging;
using System.Reflection;

namespace Omni_Airbus.Model
{
	/// <summary>
	/// Represents a sorting system in the Omni Airbus model.
	/// </summary>
	public class SortingSystem : Consumer
	{
		public int BaggageID;
		public DateTime InboundTime;
		public DateTime OutboundTime;
		private List<CheckIn> CheckIns = new List<CheckIn>();
		private List<Terminal> Terminals = new List<Terminal>();
		internal Assembly assembly = Assembly.GetExecutingAssembly();
		private Logger Log;

		/// <summary>
		/// Initializes a new instance of the SortingSystem class with specified check-ins and terminals.
		/// </summary>
		public SortingSystem(CheckIn CheckIn1, CheckIn CheckIn2, Terminal Terminal1, Terminal Terminal2) : base(null)
		{
			CheckIns.Add(CheckIn1);
			CheckIns.Add(CheckIn2);
			Terminals.Add(Terminal1);
			Terminals.Add(Terminal2);
			Log = new Logger(LoggerEnum.Information);
		}

		/// <summary>
		/// Pulls luggage from the check-in belts.
		/// </summary>
		public void PullFromBelts(object obj)
		{
			int i = 0;
			while (true)
			{
				if (Thread.CurrentThread.Name == null)
				{
					Thread.CurrentThread.Name = $"Thread: SortingSystem";
				}

				bool CheckInsExist = CheckIns[i % CheckIns.Count] != null;
				bool CheckInIsNotEmpty = CheckIns[i % CheckIns.Count].OutboundBelt.Count > 0;
				if (CheckInsExist && CheckInIsNotEmpty && CurrentLuggage != null)
				{
					CurrentLuggage = CheckIns[i % CheckIns.Count].OutboundBelt.Dequeue();
					Log.Debug($"[{Thread.CurrentThread.Name}] Current Luggage: {CurrentLuggage.baggageID}");
				}
				i++;
				Thread.Sleep(0.01f.ToMilliseconds());
			}
		}

		/// <summary>
		/// Sends the current luggage to the appropriate gate.
		/// </summary>
		public void SendToGate(object obj)
		{
			while (true)
			{
				foreach (Terminal terminal in Terminals)
				{
					if (CurrentLuggage != null && terminal.CurrentFlight != null && terminal.CurrentFlight.FlightId == CurrentLuggage.FlightID)
					{
						terminal.InboundBelt.Enqueue(CurrentLuggage);
						Log.Debug($"[{Thread.CurrentThread.Name}] Current Luggage: {CurrentLuggage.baggageID} sent to Gate: {terminal.ID}");
						CurrentLuggage = null;
						break;
					}
				}
				Thread.Sleep(0.01f.ToMilliseconds());
			}
		}
	}
}
