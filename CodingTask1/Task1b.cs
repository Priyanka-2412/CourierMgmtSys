using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CodingTask1
{
    class Task1b
    {
        static void Main()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";
            Console.Write("Enter CourierID: ");

            if (int.TryParse(Console.ReadLine(), out int courierID))
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "SELECT Weights FROM Courier WHERE CourierID = @CourierID";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.Add("@CourierID", System.Data.SqlDbType.Int).Value = courierID;
                            object result = command.ExecuteScalar();

                            if (result != null && result != DBNull.Value && double.TryParse(result.ToString(), out double weight))
                            {
                                string category;
                                if (weight <= 2.00)
                                    category = "Light";
                                else if (weight > 2.00 && weight <= 3.00)
                                    category = "Medium";
                                else
                                    category = "Heavy";

                                Console.WriteLine($"The parcel with CourierID {courierID} is categorized as: {category}");
                            }
                            else
                            {
                                Console.WriteLine("CourierID not found or weight data is missing.");
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Database error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid CourierID. Please enter a numeric value.");
            }
        }
    }
}