using System;
using System.Data.SqlClient;
using System.Threading;

namespace CodingTask2
{
    class Task2b
    {
        static void Main()
        {
            Console.Write("Enter Tracking Number: ");
            string trackingNumber = Console.ReadLine();

            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT CourierStatus FROM Courier WHERE TrackingNumber = @TrackingNumber";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result == null)
                    {
                        Console.WriteLine("Invalid tracking number. Courier not found.");
                    }
                    else
                    {
                        string status = result.ToString();
                        Console.WriteLine($"Current Status: {status}");

                        if (status == "Delivered")
                        {
                            Console.WriteLine("Courier has been delivered!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Tracking ended.");
        }
    }
}
