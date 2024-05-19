using Omni_Airbus.Controller;
using Omni_Airbus.Utils.Logging;

namespace Omni_Airbus
{
    internal class Program
    {
		/// <summary>
		/// The main entry point for the Omni-Airbus application.
		/// </summary>
		public static Logger Log = new Logger(0);
        static void Main(string[] args)
        {
            //Starting LuggageSystem in the constructor
            LuggageSystem luggageSystem = new LuggageSystem();

            //Starting FIDSSystem in the constructor
            FIDSSystem WebServer = new FIDSSystem();
            Thread logThread = new Thread(Log.LogThread);
            logThread.Start();
        }
    }
}