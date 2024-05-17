using MySqlConnector;
using Omni_Airbus.Utils;
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
        /// 
        /// </summary>
        public FIDSDisplay()
        {
            FIDSItems = new Queue<FIDSItem>();
            Thread thread = new Thread(UpdateFIDSItems);
            thread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateFIDSItems()
        {
            while (true)
            {
                FIDSItems.Clear();
                using (var conn = new MySqlConnection(MySQL.connectionString))
                {
                    string sql = "CALL GetFlightDetails()";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    conn.Open();
                    Console.WriteLine("Connection successful!");

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        int i = 0;
                        while (reader.Read())
                        {
                            if (Convert.ToDateTime(reader[0]) > DateTime.Now)
                            {
                                i++;
                                FIDSItems.Enqueue(new FIDSItem(Convert.ToDateTime(reader[0]), reader[1].ToString(), 1, reader[2].ToString()));
                                if (i > 10) break;
                            }
                        }
                    }
                }

                var jsonObject = new
                {
                    data = FIDSItems
                };

                string data = JsonSerializer.Serialize(jsonObject);
                File.WriteAllText(Path.Combine(FIDSWebServer.BASE_PATH, "departures.json"), data);
                File.WriteAllText(Path.Combine(FIDSWebServer.BASE_DEBUG_PATH, "departures.json"), data);
                Thread.Sleep(5.ToMinutes());
                Console.WriteLine("updated departures.json");
            }
        }
    }
}