using CodingTasks.Dao;
using CodingTasks.Entities;
using CodingTasks.Exception;
using CodingTasks.Util;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTasks.Dao
{
    public class CourierUserServiceImpl : ICourierUserService
    {
        private readonly string _connectionString;

        public CourierUserServiceImpl()
        {
            _connectionString = DbConnUtil.GetConnectionString();
        }

        public List<Courier> GetAllCouriers()
        {
            List<Courier> couriers = new List<Courier>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Courier";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        couriers.Add(new Courier
                        {
                            CourierID = Convert.ToInt32(reader["CourierID"]),
                            SenderName = reader["SenderName"].ToString(),
                            SenderAddress = reader["SenderAddress"].ToString(),
                            ReceiverName = reader["ReceiverName"].ToString(),
                            ReceiverAddress = reader["ReceiverAddress"].ToString(),
                            Weights = Convert.ToDouble(reader["Weight"]),
                            CourierStatus = reader["CourierStatus"].ToString(),
                            TrackingNumber = reader["TrackingNumber"].ToString(),
                            DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]),
                            LocationID = Convert.ToInt32(reader["LocationID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            ServiceID = Convert.ToInt32(reader["ServiceID"])
                        });
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
            return couriers;
        }

        public bool PlaceOrder(Courier courier)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Courier 
                                   (SenderName, SenderAddress, ReceiverName, ReceiverAddress, 
                                    Weights, CourierStatus, TrackingNumber, DeliveryDate, 
                                    LocationID, EmployeeID, ServiceID)
                                   VALUES 
                                   (@SenderName, @SenderAddress, @ReceiverName, @ReceiverAddress, 
                                    @Weights, @CourierStatus, @TrackingNumber, @DeliveryDate, 
                                    @LocationID, @EmployeeID, @ServiceID)";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@SenderName", courier.SenderName);
                    command.Parameters.AddWithValue("@SenderAddress", courier.SenderAddress);
                    command.Parameters.AddWithValue("@ReceiverName", courier.ReceiverName);
                    command.Parameters.AddWithValue("@ReceiverAddress", courier.ReceiverAddress);
                    command.Parameters.AddWithValue("@Weights", courier.Weights);
                    command.Parameters.AddWithValue("@CourierStatus", courier.CourierStatus ?? "Processing");
                    command.Parameters.AddWithValue("@TrackingNumber", courier.TrackingNumber);
                    command.Parameters.AddWithValue("@DeliveryDate", courier.DeliveryDate);
                    command.Parameters.AddWithValue("@LocationID", courier.LocationID);
                    command.Parameters.AddWithValue("@EmployeeID", courier.EmployeeID);
                    command.Parameters.AddWithValue("@ServiceID", courier.ServiceID);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }

        public string GetOrderStatus(string trackingNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT CourierStatus FROM Courier WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found.");
                    }

                    return result.ToString();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }

        public bool CancelOrder(string trackingNumber)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "UPDATE Courier SET CourierStatus = 'Cancelled' WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new TrackingNumberNotFoundException($"Tracking number {trackingNumber} not found.");
                    }

                    return true;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }

        public List<Courier> GetAssignedOrder(int employeeID)
        {
            List<Courier> assignedOrders = new List<Courier>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Courier WHERE EmployeeID = @EmployeeID";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        assignedOrders.Add(new Courier
                        {
                            CourierID = Convert.ToInt32(reader["CourierID"]),
                            SenderName = reader["SenderName"].ToString(),
                            ReceiverName = reader["ReceiverName"].ToString(),
                            CourierStatus = reader["CourierStatus"].ToString(),
                            TrackingNumber = reader["TrackingNumber"].ToString(),
                            DeliveryDate = reader["DeliveryDate"] != DBNull.Value? Convert.ToDateTime(reader["DeliveryDate"]): DateTime.MinValue
                        });
                    }
                }

                if (assignedOrders.Count == 0)
                {
                    throw new InvalidEmployeeIdException($"No assigned orders found for Employee ID {employeeID}.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }

            return assignedOrders;
        }

        public List<Courier> RetrieveDeliveryHistory(string trackingNumber)
        {
            List<Courier> history = new List<Courier>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM Courier WHERE TrackingNumber = @TrackingNumber";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@TrackingNumber", trackingNumber);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        history.Add(new Courier
                        {
                            CourierID = Convert.ToInt32(reader["CourierID"]),
                            SenderName = reader["SenderName"].ToString(),
                            SenderAddress = reader["SenderAddress"].ToString(),
                            ReceiverName = reader["ReceiverName"].ToString(),
                            ReceiverAddress = reader["ReceiverAddress"].ToString(),
                            Weights = Convert.ToDouble(reader["Weights"]),
                            CourierStatus = reader["CourierStatus"].ToString(),
                            TrackingNumber = reader["TrackingNumber"].ToString(),
                            DeliveryDate = Convert.ToDateTime(reader["DeliveryDate"]),
                            LocationID = Convert.ToInt32(reader["LocationID"]),
                            EmployeeID = Convert.ToInt32(reader["EmployeeID"]),
                            ServiceID = Convert.ToInt32(reader["ServiceID"])
                        });
                    }

                }

                return history;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }

        public decimal GenerateRevenueReport()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string query = "SELECT SUM(Amount) as TotalRevenue FROM Payment";
                    SqlCommand command = new SqlCommand(query, connection);

                    connection.Open();
                    object result = command.ExecuteScalar();

                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                throw;
            }
        }
    }
}