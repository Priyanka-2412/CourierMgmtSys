using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTask1
{
    class Task1a1b
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            //Task1a: Check courier status
            Console.Write("Enter Courier ID to check status: ");
            if (!int.TryParse(Console.ReadLine(), out int statusCourierID))
            {
                Console.WriteLine("Invalid Courier ID. Please enter a numeric value.");
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string statusQuery = "SELECT CourierStatus FROM Courier WHERE CourierID = @CourierID";

                    using (SqlCommand command = new SqlCommand(statusQuery, connection))
                    {
                        command.Parameters.AddWithValue("@CourierID", statusCourierID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string courierStatus = reader["CourierStatus"].ToString();

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
                                Console.WriteLine("Courier ID not found for status check.");
                            }
                        }
                    }

                    //Task1b: Check weight
                    Console.Write("\nEnter Courier ID to check weight: ");
                    if (!int.TryParse(Console.ReadLine(), out int weightCourierID))
                    {
                        Console.WriteLine("Invalid Courier ID. Please enter a numeric value.");
                        return;
                    }

                    string weightQuery = "SELECT Weights FROM Courier WHERE CourierID = @CourierID";

                    using (SqlCommand weightCommand = new SqlCommand(weightQuery, connection))
                    {
                        weightCommand.Parameters.AddWithValue("@CourierID", weightCourierID);
                        using (SqlDataReader weightReader = weightCommand.ExecuteReader())
                        {
                            if (weightReader.Read())
                            {
                                double weight = weightReader["Weights"] != DBNull.Value ? Convert.ToDouble(weightReader["Weights"]) : -1;

                                if (weight >= 0)
                                {
                                    string category = weight <= 2.00 ? "Light" : (weight <= 3.00 ? "Medium" : "Heavy");
                                    Console.WriteLine($"The parcel is categorized as: {category}");
                                }
                                else
                                {
                                    Console.WriteLine("Weight data is missing.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Courier ID not found for weight check.");
                            }
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