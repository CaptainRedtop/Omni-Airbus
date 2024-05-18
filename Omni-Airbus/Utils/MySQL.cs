﻿using MySqlConnector;

namespace Omni_Airbus.Utils
{
    public static class MySQL
    {
        public const string connectionString = "Server=localhost;" +
                                               "Port=3306;" +
                                               "Database=airport_schedule;" +
                                               "User ID=airport;" +
                                               "Password=password;";

        public static List<object[]> DBStatement(string sql)
        {
            using (var conn = new MySqlConnection(MySQL.connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                Console.WriteLine("Connection successful!");

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<object[]> rows = new List<object[]>();
                    while (reader.Read())
                    {
                        object[] val = new object[reader.FieldCount];
                        reader.GetValues(val);
                        rows.Add(val);
                    }
                    return rows;
                }
            }
        }
    }
}
