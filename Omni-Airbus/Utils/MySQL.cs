using MySqlConnector;
using Omni_Airbus.Utils.Logging;

namespace Omni_Airbus.Utils
{
	/// <summary>
	/// Utility class for interacting with a MySQL database.
    /// DBStatement takes an SQL string.
	/// </summary>
	public static class MySQL
    {
        private static Logger Log = new Logger(LoggerEnum.Information);
        public const string connectionString = "Server=localhost;" +
                                               "Port=3306;" +
                                               "Database=airport_schedule;" +
                                               "User ID=airport;" +
                                               "Password=password;";

		/// <summary>
		/// Executes a SQL statement against the MySQL database and returns the result as a list of object arrays.
		/// </summary>
		/// <param name="sql">The SQL query to execute.</param>
		/// <returns>A list of object arrays representing the rows returned by the query.</returns>
		public static List<object[]> DBStatement(string sql)
        {
            using (var conn = new MySqlConnection(MySQL.connectionString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                Log.Information($"Database Connection successful");

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    List<object[]> rows = new List<object[]>();
                    while (reader.Read())
                    {
                        object[] val = new object[reader.FieldCount];
                        reader.GetValues(val);
                        rows.Add(val);
                    }
                    Log.Information($"Command returned {rows.Count} rows");
                    return rows;
                }
            }
        }
    }
}
