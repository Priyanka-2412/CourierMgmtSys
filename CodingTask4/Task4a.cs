using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodingTask4
{
    class Task4a
    {
        static string[,] trackingData;
        static Dictionary<string, string> trackingMap = new Dictionary<string, string>();

        static string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

        static void Main()
        {
            Console.WriteLine("========== Parcel Tracking System ==========");

            // Step 1: Load data from DB
            LoadTrackingData();

            // Step 2: User input
            Console.Write("\nEnter the Tracking Number: ");
            string userInput = Console.ReadLine().Trim().ToUpper();

            // Step 3: Simulate tracking
            TrackParcel(userInput);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static void LoadTrackingData()
        {
            List<string[]> tempList = new List<string[]>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT TrackingNumber, CourierStatus FROM Courier";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        string trackingNumber = reader["TrackingNumber"].ToString().ToUpper();
                        string status = reader["CourierStatus"].ToString();

                        tempList.Add(new string[] { trackingNumber, status });
                        trackingMap[trackingNumber] = status;
                    }
                    reader.Close();
                }

                trackingData = new string[tempList.Count, 2];
                for (int i = 0; i < tempList.Count; i++)
                {
                    trackingData[i, 0] = tempList[i][0];
                    trackingData[i, 1] = tempList[i][1];
                }

                Console.WriteLine($"\nLoaded {tempList.Count} records from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n Error loading tracking data: " + ex.Message);
            }
        }

        static void TrackParcel(string trackingNumber)
        {
            if (trackingMap.ContainsKey(trackingNumber))
            {
                string status = trackingMap[trackingNumber];
                Console.WriteLine($"\nTracking Number: {trackingNumber}");
                Console.WriteLine($"Current Status: {status}");

                switch (status.ToLower())
                {
                    case "in transit":
                        Console.WriteLine("Your parcel is currently *In Transit*.");
                        break;
                    case "out for delivery":
                        Console.WriteLine("Your parcel is *Out for Delivery*.");
                        break;
                    case "delivered":
                        Console.WriteLine("Your parcel has been *Delivered*.");
                        break;
                    case "pending":
                        Console.WriteLine("Parcel is still *Pending*.");
                        break;
                    case "processing":
                        Console.WriteLine("Parcel is in *Processing*.");
                        break;
                    case "shipped":
                        Console.WriteLine("Parcel has been *Shipped*.");
                        break;
                    default:
                        Console.WriteLine("Status: " + status);
                        break;
                }
            }
            else
            {
                Console.WriteLine("\n Tracking Number not found. Please check again.");
            }
        }
    }
}