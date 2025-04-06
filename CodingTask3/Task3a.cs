using System;
using System.Data.SqlClient;

namespace CodingTask3
{
    class Task3a
    {
        static void Main()
        {
            Console.Write("Enter Courier History ID: ");
            string historyId = Console.ReadLine();

            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT LocationUpdate, UpdatedTime FROM TrackingHistory WHERE HistoryID = @HistoryID ORDER BY UpdatedTime";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@HistoryID", historyId);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    bool hasData = false;
                    Console.WriteLine("\nTracking Details:\n");

                    while (reader.Read())
                    {
                        hasData = true;
                        string location = reader["LocationUpdate"].ToString();
                        string time = reader["UpdatedTime"].ToString();

                        Console.WriteLine($"Location: {location} \t Time: {time}");
                    }

                    if (!hasData)
                    {
                        Console.WriteLine("No tracking data found for the given History ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching tracking data:\n" + ex.Message);
            }

            Console.WriteLine("\n Press any key to exit...");
            Console.ReadKey();
        }
    }
}