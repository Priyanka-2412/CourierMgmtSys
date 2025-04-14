using System;
using System.Data;
using System.Data.SqlClient;

namespace CodingTask2
{
    class Task2a
    {
        static void Main()
        {
            Console.Write("Enter CourierID: ");
            string courierId = Console.ReadLine();

            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            string query = "SELECT CourierID, SenderName, ReceiverName, DeliveryDate, CourierStatus FROM Courier WHERE CourierID = @CourierID";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@CourierID", courierId);

                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    int rowCount = dt.Rows.Count;

                    if (rowCount == 0)
                    {
                        Console.WriteLine("No courier records found for the given Courier ID.");
                        return;
                    }

                    Console.WriteLine("\nDetails for Courier ID: " + courierId);
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