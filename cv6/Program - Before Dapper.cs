using Microsoft.Data.Sqlite;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string createSQL = """
                CREATE TABLE "Customer" (
                	"Id"	INTEGER,
                	"Name"	TEXT NOT NULL,
                	"Address"	TEXT,
                	PRIMARY KEY("Id" AUTOINCREMENT)
                );

                CREATE TABLE "Order" (
                	"Id"	INTEGER,
                	"CustomerId"	INTEGER NOT NULL,
                	"Product"	TEXT NOT NULL,
                	"Price"	NUMERIC NOT NULL,
                	FOREIGN KEY("CustomerId") REFERENCES "Customer"("Id"),
                	PRIMARY KEY("Id" AUTOINCREMENT)
                );
                """;

            string connectinString = "Data Source=mydb.db;";

            using SqliteConnection connection = new SqliteConnection(connectinString);

            connection.Open();


            /*using SqliteCommand cmd = new SqliteCommand();
            cmd.CommandText = createSQL;
            cmd.Connection = connection;

            cmd.ExecuteNonQuery();*/

            /*using SqliteCommand insertCmd = new SqliteCommand("INSERT INTO Customer (Name, Address) VALUES ('Jan', 'Ostrava')", connection);
            insertCmd.ExecuteNonQuery();*/

            using SqliteTransaction transaction = connection.BeginTransaction();

            using SqliteCommand insertCmd = new SqliteCommand("INSERT INTO Customer (Name, Address) VALUES (@Jmeno, @Adresa)", connection, transaction);
            insertCmd.Parameters.AddWithValue("Jmeno", "Zuzana");
            //insertCmd.Parameters.AddWithValue("Adresa", "Ostrava");
            insertCmd.Parameters.Add(new SqliteParameter()
            {
                ParameterName = "Adresa",
                Value = DBNull.Value,
                DbType = System.Data.DbType.String
            });
            insertCmd.ExecuteNonQuery();

            using SqliteCommand selectCmd = new SqliteCommand("SELECT * FROM Customer", connection, transaction);
            using SqliteDataReader reader = selectCmd.ExecuteReader();
            while (reader.Read())
            {
                int id = reader.GetInt32(reader.GetOrdinal("Id"));
                string name = reader.GetString(reader.GetOrdinal("Name"));
                string address = null;
                if (!reader.IsDBNull(reader.GetOrdinal("Address")))
                {
                    address = reader.GetString(reader.GetOrdinal("Address"));
                }
                Console.WriteLine(id + " | " + name + " | " + address);
            }

            using SqliteCommand countCmd = new SqliteCommand("SELECT COUNT(*) FROM Customer", connection, transaction);
            long count = (long)countCmd.ExecuteScalar();

            Console.WriteLine($"count: {count}");

            transaction.Commit();
        }
    }
}
