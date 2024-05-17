using Omni_Airbus.Model.FIDS;
using MySqlConnector;
using System.Text.Json;

namespace Omni_Airbus.Utils
{
    public class MySQL
    {
        Queue<FIDSItem> FIDSItems { get; set; }

        public MySQL(string statement)
        {
            FIDSItems = new Queue<FIDSItem>();
            Thread thread = new Thread(() => DBStatement(statement));
            thread.Start();
        }

        public const string connectionString = "Server=localhost;" +
                                               "Port=3306;" +
                                               "Database=airport_schedule;" +
                                               "User ID=airport;" +
                                               "Password=password!;";

        public void DBStatement(string sql)
        {
            while (true)
            {
                FIDSItems.Clear();
                using (var conn = new MySqlConnection(MySQL.connectionString))
                {
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
