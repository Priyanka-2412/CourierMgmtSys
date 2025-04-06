using System;
using System.Data.SqlClient;

namespace CodingTask3
{
    class Task3b
    {
        static void Main()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            try
            {
                Console.Write("Enter New Order Pickup Address: ");
                string newPickupAddress = Console.ReadLine();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT CourierID, SenderName, SenderAddress 
                        FROM Courier
                        WHERE CourierStatus IN ('In Transit', 'Shipped')
                        AND SenderAddress LIKE @pickupAddress
                        ORDER BY NEWID();"; // Random selection from possible matches

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@pickupAddress", "%" + newPickupAddress + "%");

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int courierId = reader["CourierID"] != DBNull.Value ? Convert.ToInt32(reader["CourierID"]) : 0;
                            string senderName = reader["SenderName"]?.ToString();
                            string senderAddress = reader["SenderAddress"]?.ToString();

                            Console.WriteLine("\nNearest Courier Found:");
                            Console.WriteLine($"ID: {courierId}");
                            Console.WriteLine($"Sender: {senderName}");
                            Console.WriteLine($"Address: {senderAddress}");
                        }
                        else
                        {
                            Console.WriteLine("No available courier found for that location.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}