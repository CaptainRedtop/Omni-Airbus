using MySqlConnector;
using Omni_Airbus.Utils;
using Omni_Airbus.Utils.Logger;
using System.Text.Json;

namespace Omni_Airbus.Model.FIDS
{
    /// <summary>
    /// <c>FIDSDisplay</c> Controls what will be displayed in the view
    /// </summary>
    public class FIDSDisplay
    {
        Queue<FIDSItem> FIDSItems { get; set; }

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
                FIDSItems.Clear();
                List<object[]> sqlData = MySQL.DBStatement("CALL GetFlightDetails();");

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
        }
    }
}