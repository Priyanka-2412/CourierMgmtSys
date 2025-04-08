using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodingTask1
{
    class Task1a
    {
        static void Main()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            Console.Write("Enter Courier ID: ");
            string courierId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(courierId))
            {
                Console.WriteLine("Invalid Courier ID.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT CourierStatus FROM Courier WHERE CourierID = @CourierID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@CourierID", courierId);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            string courierStatus = result.ToString();

                            switch (courierStatus)
                            {
                                case "Delivered":
                                    Console.WriteLine("The order has been Delivered.");
                                    break;
                                case "In Transit":
                                    Console.WriteLine("The order is In Transit.");
                                    break;
                                case "Shipped":
                                    Console.WriteLine("The order has been Shipped.");
                                    break;
                                case "Pending":
                                    Console.WriteLine("The order is Pending.");
                                    break;
                                default:
                                    Console.WriteLine("Unknown status.");
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Courier ID not found.");
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
    }
}