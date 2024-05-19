using Omni_Airbus.Model;
using Omni_Airbus.Model.Booking;
using Omni_Airbus.Utils;
using Omni_Airbus.Utils.Logging;

/// <summary>
/// Represents the core logic for managing the luggage system in an airport scenario.
/// </summary>
namespace Omni_Airbus.Controller
{
	internal class LuggageSystem
    {
        private static ConveyerBelt CheckInBelt1 = new ConveyerBelt(1);
        private static ConveyerBelt CheckInBelt2 = new ConveyerBelt(2);
        private static ConveyerBelt TerminalBelt1 = new ConveyerBelt(3);
        private static ConveyerBelt TerminalBelt2 = new ConveyerBelt(4);

        private static CheckIn CheckIn1 = new CheckIn(CheckInBelt1, 1);
        private static CheckIn CheckIn2 = new CheckIn(CheckInBelt2, 2);

        private static Terminal Terminal1 = new Terminal(TerminalBelt1, CheckIn1, CheckIn2, 1);
        private static Terminal Terminal2 = new Terminal(TerminalBelt2, CheckIn1, CheckIn2, 2);

        private static SortingSystem SortingSystem = new SortingSystem(CheckIn1, CheckIn2, Terminal1, Terminal2);
        public static Queue<Flight> Flights;



        public LuggageSystem()
        {
            RunLuggageSystem();
        }
		/// <summary>
		/// Initializes the luggage system and starts the threads.
		/// </summary>
		public void RunLuggageSystem()
        {
            Flights = Flight.GetFlights();
            Thread CheckIn1Thread = new Thread(CheckIn1.Checked);
            Thread CheckIn2Thread = new Thread(CheckIn2.Checked);

            ThreadPool.QueueUserWorkItem(SortingSystem.PullFromBelts);
            ThreadPool.QueueUserWorkItem(SortingSystem.SendToGate);
            ThreadPool.QueueUserWorkItem(Terminal1.Pull);
            ThreadPool.QueueUserWorkItem(Terminal2.Pull);
            ThreadPool.QueueUserWorkItem(die);
            ThreadPool.SetMaxThreads(4, 2);

            CheckIn1Thread.Start();
            CheckIn2Thread.Start();
            //Terminal1Thread.Start();
            //Terminal2Thread.Start();
            //SortingSystemPullThread.Start();
            //SortingSystemSendThread.Start();
        }

        public static bool GetFlightId(int TerminalId, out int result)
        {
            result = 0;
            if (TerminalId == 1)
            {
                if (Terminal1.CurrentFlight != null)
                {
                    result = Terminal1.CurrentFlight.FlightId;
                }
            }
            else
            {
                if (Terminal2.CurrentFlight != null)
                {
                    result = Terminal2.CurrentFlight.FlightId;
                }
            }

            if (result == 0)
            {
                return false;
            }
            return true;
        }

        public static void die(object obj)  // Kills the threads and exits
        {
            while (true)
            {
                if (CheckIn.GetLuggageCheckedIn() > 10000)
                {
                    Report.GenerateReport();
                    Environment.Exit(0);
                }
                Thread.Sleep(1f.ToMilliseconds());
            }
        }
    }
}
