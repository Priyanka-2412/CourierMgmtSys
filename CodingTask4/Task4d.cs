using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodingTask4
{
    class Task4d
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Tracking Number:");
            string trackingNumber = Console.ReadLine().Trim().ToUpper();

            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    Order order = OrderService.GetOrderDetails(trackingNumber, conn);

                    if (order != null)
                    {
                        string[,] deliveryMatrix = GetDeliveryMatrix();
                        Dictionary<string, string> statusMap = GetTrackingStatusMap();

                        string email = EmailGenerator.GenerateEmail(order);
                        string status = GetStatusFromMap(statusMap, trackingNumber);
                        string deliveryRoute = GetRouteFromMatrix(deliveryMatrix, order.CourierID);

                        Console.WriteLine("\n--- Order Confirmation Email ---\n");
                        Console.WriteLine(email);
                        Console.WriteLine($"Current Delivery Status: {status}");
                        Console.WriteLine($"Delivery Route Info     : {deliveryRoute}");
                    }
                    else
                    {
                        Console.WriteLine("Order not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.ReadKey();
        }

        static string[,] GetDeliveryMatrix()
        {
            return new string[3, 3]
            {
                { "Warehouse", "In Transit", "Destination Hub" },
                { "Packing", "Out for Delivery", "Delivered" },
                { "Queued", "Sorting", "Dispatched" }
            };
        }

        static Dictionary<string, string> GetTrackingStatusMap()
        {
            return new Dictionary<string, string>
            {
                { "TRK123", "In Transit" },
                { "TRK456", "Out for Delivery" },
                { "TRK789", "Delivered" },
                { "TRK001", "Sorting" }
            };
        }

        static string GetStatusFromMap(Dictionary<string, string> map, string trackingNumber)
        {
            return map.ContainsKey(trackingNumber) ? map[trackingNumber] : "Unknown";
        }

        static string GetRouteFromMatrix(string[,] matrix, int courierId)
        {
            int row = courierId % 3;
            int col = (courierId / 3) % 3;
            return matrix[row, col];
        }
    }

    public class Order
    {
        public int CourierID { get; set; }
        public string CustomerName { get; set; }
        public string ReceiverAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string TrackingNumber { get; set; }
    }

    public class OrderService
    {
        public static Order GetOrderDetails(string trackingNumber, SqlConnection conn)
        {
            Order order = null;

            string query = @"
                SELECT c.CourierID, u.Names, c.ReceiverAddress, c.DeliveryDate, c.TrackingNumber
                FROM Courier c
                JOIN Users u ON c.UserID = u.UserID
                WHERE c.TrackingNumber = @TrackingNumber";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = new Order
                        {
                            CourierID = reader.GetInt32(0),
                            CustomerName = reader.GetString(1),
                            ReceiverAddress = reader.GetString(2),
                            DeliveryDate = reader.GetDateTime(3),
                            TrackingNumber = reader.GetString(4)
                        };
                    }
                }
            }

            return order;
        }
    }

    public class EmailGenerator
    {
        public static string GenerateEmail(Order order)
        {
            string customerName = order.CustomerName;

            return $@"
Subject: Order Confirmation - Tracking No: {order.TrackingNumber}

Dear {customerName},

Thank you for your order! Here are your delivery details:

Order Number      : {order.CourierID}
Delivery Address  : {order.ReceiverAddress}
Expected Delivery : {order.DeliveryDate:dddd, dd MMMM yyyy}

We appreciate your business!

Warm regards,
Courier Management Team
";
        }
    }
}