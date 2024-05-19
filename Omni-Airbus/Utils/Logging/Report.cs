using Omni_Airbus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Utils.Logging
{
    public class Report
    {
        public static HashSet<int> Aircrafts = new HashSet<int>();
        public static HashSet<int> Flights = new HashSet<int>();
        public static HashSet<int> Desinations = new HashSet<int>();
        public static HashSet<int> Airlines = new HashSet<int>();

        public static void GenerateReport()
        {
            Logger Log = new Logger(LoggerEnum.Report);
            Log.Report($"");
            Log.Report($"Aircrafts: {Aircrafts.Count}");
            Log.Report($"Flights: {Flights.Count}");
            Log.Report($"Luggage passed through the airport: {CheckIn.GetLuggageCheckedIn()}");
            Log.Report($"Destinations: {Desinations.Count}");
            Log.Report($"Airlines: {Airlines.Count}");

            Console.WriteLine($"");
            Console.WriteLine($"Aircrafts: {Aircrafts.Count}");
            Console.WriteLine($"Flights: {Flights.Count}");
            Console.WriteLine($"Luggage passed through the airport: {CheckIn.GetLuggageCheckedIn()}");
            Console.WriteLine($"Destinations: {Desinations.Count}");
            Console.WriteLine($"Airlines: {Airlines.Count}");
        }
    }
}
