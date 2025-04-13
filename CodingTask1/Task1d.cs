using System;
using System.Data;
using System.Data.SqlClient;

namespace CodingTask1
{
    class Task1d
    {
        private static string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

        static void Main(string[] args)
        {
            DisplayAllCourierIDsAssignedToEmployees();
        }

        static void DisplayAllCourierIDsAssignedToEmployees()
        {
            string query = "SELECT EmployeeID, CourierID FROM Courier";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Console.WriteLine("CourierIDs and their corresponding EmployeeIDs:");

                        while (reader.Read())
                        {
                            Console.WriteLine($"CourierID {reader["CourierID"]} assigned to EmployeeID {reader["EmployeeID"]}");

                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found in the Courier table.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}