using System;
using System.Data;
using System.Data.SqlClient;

namespace CodingTask2
{
    class Task2a
    {
        static void Main()
        {
            Console.Write("Enter UserID: ");
            string userId = Console.ReadLine();

            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            string query = "SELECT CourierID, SenderName, ReceiverName, DeliveryDate, CourierStatus FROM Courier WHERE UserID = @UserID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    int rowCount = dt.Rows.Count;

                    if (rowCount == 0)
                    {
                        Console.WriteLine("No courier records found for the given Customer ID.");
                        return;
                    }

                    Console.WriteLine("\n All courier details for a specific customer.  : " + userId);
                    for (int i = 0; i < rowCount; i++)
                    {
                        Console.WriteLine($"Courier ID: {dt.Rows[i]["CourierID"]}, Sender: {dt.Rows[i]["SenderName"]}, Receiver: {dt.Rows[i]["ReceiverName"]}, Date: {dt.Rows[i]["DeliveryDate"]}, Status: {dt.Rows[i]["CourierStatus"]}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
        }
    }
}