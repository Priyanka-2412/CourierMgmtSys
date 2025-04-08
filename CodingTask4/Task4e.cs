using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodingTask4
{
    public class Task4e
    {
        static string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

        public static void Main(string[] args)
        {
            CalculateShippingCost();
        }

        public static void CalculateShippingCost()
        {
            Console.WriteLine("Enter Courier ID:");
            if (!int.TryParse(Console.ReadLine(), out int courierId))
            {
                Console.WriteLine("Invalid Courier ID input.");
                return;
            }

            string source = "";
            string destination = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Database connected successfully.");

                    string query = "SELECT SenderAddress, ReceiverAddress FROM Courier WHERE CourierId = @CourierId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@CourierId", courierId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                source = reader["SenderAddress"].ToString();
                                destination = reader["ReceiverAddress"].ToString();
                                Console.WriteLine($"\nSender Address: {source}");
                                Console.WriteLine($"Receiver Address: {destination}");
                            }
                            else
                            {
                                Console.WriteLine("Courier ID not found.");
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database operation failed: " + ex.Message);
                    return;
                }
            }

            Console.WriteLine("\nEnter Parcel Weight (in kg):");
            if (!double.TryParse(Console.ReadLine(), out double weight))
            {
                Console.WriteLine("Invalid weight input.");
                return;
            }

            double distance = EstimateDistance(source, destination);
            double costPerKmPerKg = 0.05;
            double totalCost = distance * weight * costPerKmPerKg;

            Console.WriteLine($"\nEstimated Distance: {distance} km");
            Console.WriteLine($"Cost per km per kg: ${costPerKmPerKg}");
            Console.WriteLine($"Parcel Weight: {weight} kg");
            Console.WriteLine($"Calculated Shipping Cost: ${Math.Round(totalCost, 2)}");

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static double EstimateDistance(string from, string to)
        {
            from = from.ToLower();
            to = to.ToLower();

            if ((from.Contains("bangalore") && to.Contains("delhi")) ||
                (from.Contains("delhi") && to.Contains("bangalore")))
                return 2129.4;
            else if ((from.Contains("delhi") && to.Contains("mumbai")) ||
                     (from.Contains("mumbai") && to.Contains("delhi")))
                return 1384;
            else if ((from.Contains("mumbai") && to.Contains("hyderabad")) ||
                     (from.Contains("hyderabad") && to.Contains("mumbai")))
                return 705;
            else if ((from.Contains("hyderabad") && to.Contains("chandigarh")) ||
                     (from.Contains("chandigarh") && to.Contains("hyderabad")))
                return 1955;
            else if ((from.Contains("chandigarh") && to.Contains("pune")) ||
                     (from.Contains("pune") && to.Contains("chandigarh")))
                return 1647;
            else if ((from.Contains("pune") && to.Contains("kolkata")) ||
                     (from.Contains("kolkata") && to.Contains("pune")))
                return 1876;
            else if ((from.Contains("kolkata") && to.Contains("lucknow")) ||
                     (from.Contains("lucknow") && to.Contains("kolkata")))
                return 1072;
            else if ((from.Contains("lucknow") && to.Contains("surat")) ||
                     (from.Contains("surat") && to.Contains("lucknow")))
                return 1260;
            else if ((from.Contains("surat") && to.Contains("chennai")) ||
                     (from.Contains("chennai") && to.Contains("surat")))
                return 1587;
            else if ((from.Contains("chennai") && to.Contains("jaipur")) ||
                     (from.Contains("jaipur") && to.Contains("chennai")))
                return 2051;

            Console.WriteLine("Distance between given locations is not defined. Using default distance of 1000 km.");
            return 1000; // Default fallback distance
        }
    }
}
