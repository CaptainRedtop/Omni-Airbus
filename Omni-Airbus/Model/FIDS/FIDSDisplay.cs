using MySqlConnector;
using Omni_Airbus.Controller;
using Omni_Airbus.Model.Booking;
using Omni_Airbus.Utils;
using Omni_Airbus.Utils.Logging;
using System.Text.Json;

namespace Omni_Airbus.Model.FIDS
{
    /// <summary>
    /// <c>FIDSDisplay</c> Controls what will be displayed in the view
    /// </summary>
    public class FIDSDisplay
    {
        Queue<FIDSItem> FIDSItems { get; set; }
        Logger Log = new Logger(LoggerEnum.Information);

        /// <summary>
        /// Start the UpdateFIDSItems thread.
        /// </summary>
        public FIDSDisplay()
        {
            FIDSItems = new Queue<FIDSItem>();
            Thread thread = new Thread(UpdateFIDSItems);
            thread.Start();
        }

        /// <summary>
        /// Update the Fids Items
        /// </summary>
        public void UpdateFIDSItems()
        {
            while (true)
            {
                bool flight1 = LuggageSystem.GetFlightId(1, out int flightId1);
                bool flight2 = LuggageSystem.GetFlightId(2, out int flightId2);
                Log.Debug($"Flight 1 at terminal: {flight1} | Flight 2 at terminal: {flight1}");
                if (flight1 && flight2)
                {
                    Log.Debug($"Flight 1: {flightId1} | Flight 2{flightId2}");
                    FIDSItems.Clear();
                    List<object[]> sqlData = MySQL.DBStatement($"CALL GetFlightDetails({flightId1},{flightId2});");

                    DateTime now = DateTime.Now;
                    var nextFlights = sqlData
                        .Where(row => (DateTime)row[0] > now)          // Filter for flight times after now
                        .OrderBy(row => (DateTime)row[0])              // Sort by flight time
                        .Take(15)                                      // Take the next 15 flights
                        .ToList();

                    foreach (object[] row in nextFlights)
                    {
                        DateTime flightTime = (DateTime)row[0];
                        FIDSItems.Enqueue(new FIDSItem(flightTime, row[1].ToString(), 1, row[2].ToString()));
                    }

                    var jsonObject = new
                    {
                        data = FIDSItems
                    };

                    string jsonData = JsonSerializer.Serialize(jsonObject);
                    File.WriteAllText(Path.Combine(FIDSWebServer.BASE_PATH, "departures.json"), jsonData);
                    File.WriteAllText(Path.Combine(FIDSWebServer.BASE_DEBUG_PATH, "departures.json"), jsonData);

                    Thread.Sleep(1.ToMilliseconds());
                }
                Thread.Sleep(1.ToMilliseconds());
            }
        }
    }
}