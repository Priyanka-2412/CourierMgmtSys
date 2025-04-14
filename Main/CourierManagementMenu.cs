using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingTasks.Dao;
using CodingTasks.Entities;

namespace CodingTasks.Main
{
   
    
   public class CourierManagementMenu
    {
       
            private readonly ICourierUserService _courierService;
            private readonly ICourierAdminService _adminService;

            public CourierManagementMenu(ICourierUserService courierService, ICourierAdminService adminService)
            {
                _courierService = courierService;
                _adminService = adminService;
            }

            public void DisplayMenu()
            {
                while (true)
                {
                    Console.WriteLine("\nCourier Service Menu:");
                    Console.WriteLine("1. Place an order");
                    Console.WriteLine("2. Get order status");
                    Console.WriteLine("3. Cancel an order");
                    Console.WriteLine("4. Get assigned orders");
                    Console.WriteLine("5. View delivery history");
                    Console.WriteLine("6. Generate revenue report");
                    Console.WriteLine("7. Add courier staff (Admin)");
                    Console.WriteLine("8. Exit");

                    Console.Write("Enter your choice: ");
                    string choice = Console.ReadLine();

                    try
                    {
                        switch (choice)
                        {
                            case "1":
                                PlaceOrder();
                                break;
                            case "2":
                                GetOrderStatus();
                                break;
                            case "3":
                                CancelOrder();
                                break;
                            case "4":
                                GetAssignedOrders();
                                break;
                            case "5":
                                ViewDeliveryHistory();
                                break;
                            case "6":
                                GenerateRevenueReport();
                                break;
                            case "7":
                                AddCourierStaff();
                                break;
                            case "8":
                                Console.WriteLine("Exiting program...");
                                return;
                            default:
                                Console.WriteLine("Invalid choice. Please enter a number between 1 and 8.");
                                break;
                        }
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine($"An error occurred: {ex.Message}");
                    }
                }
            }

            private void PlaceOrder()
            {
                Console.WriteLine("\n--- Place New Order ---");

                Courier courier = new Courier();

                Console.Write("Enter Sender Name: ");
                courier.SenderName = Console.ReadLine();

                Console.Write("Enter Sender Address: ");
                courier.SenderAddress = Console.ReadLine();

                Console.Write("Enter Receiver Name: ");
                courier.ReceiverName = Console.ReadLine();

                Console.Write("Enter Receiver Address: ");
                courier.ReceiverAddress = Console.ReadLine();

                Console.Write("Enter Weights (kg): ");
                courier.Weights = double.Parse(Console.ReadLine());

                Console.Write("Enter Tracking Number: ");
                courier.TrackingNumber = Console.ReadLine();

                Console.Write("Enter Delivery Date (YYYY-MM-DD): ");
                courier.DeliveryDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter Location ID: ");
                courier.LocationID = int.Parse(Console.ReadLine());

                Console.Write("Enter Employee ID: ");
                courier.EmployeeID = int.Parse(Console.ReadLine());

                Console.Write("Enter Service ID: ");
                courier.ServiceID = int.Parse(Console.ReadLine());

                bool success = _courierService.PlaceOrder(courier);
                Console.WriteLine(success ? "Order placed successfully!" : "Failed to place order.");
            }

            private void GetOrderStatus()
            {
                Console.WriteLine("\n--- Check Order Status ---");
                Console.Write("Enter Tracking Number: ");
                string trackingNumber = Console.ReadLine();

                string status = _courierService.GetOrderStatus(trackingNumber);
                Console.WriteLine($"Current CourierStatus: {status}");
            }

            private void CancelOrder()
            {
                Console.WriteLine("\n--- Cancel Order ---");
                Console.Write("Enter Tracking Number: ");
                string trackingNumber = Console.ReadLine();

                bool success = _courierService.CancelOrder(trackingNumber);
                Console.WriteLine(success ? "Order cancelled successfully!" : "Failed to cancel order.");
            }

            private void GetAssignedOrders()
            {
                Console.WriteLine("\n--- View Assigned Orders ---");
                Console.Write("Enter Employee ID: ");
                int employeeId = int.Parse(Console.ReadLine());

                var orders = _courierService.GetAssignedOrder(employeeId);

                if (orders.Count == 0)
                {
                    Console.WriteLine("No orders assigned to this employee.");
                    return;
                }

                Console.WriteLine($"\nAssigned Orders ({orders.Count}):");
                foreach (var order in orders)
                {
                    Console.WriteLine($"- {order.TrackingNumber}: {order.CourierStatus} (Due: {order.DeliveryDate.ToShortDateString()})");
                }
            }

            private void ViewDeliveryHistory()
            {
                Console.WriteLine("\n--- View Delivery History ---");
                Console.Write("Enter Tracking Number: ");
                string trackingNumber = Console.ReadLine();

                var history = _courierService.RetrieveDeliveryHistory(trackingNumber);

                if (history.Count == 0)
                {
                    Console.WriteLine("No delivery history found for this tracking number.");
                    return;
                }

                Console.WriteLine($"\nDelivery History ({history.Count} records):");
                foreach (var record in history)
                {
                    Console.WriteLine($"- ID: {record.CourierID}, CourierStatus: {record.CourierStatus}, Delivery Date: {record.DeliveryDate.ToShortDateString()}");
                    Console.WriteLine($"  From: {record.SenderName} ({record.SenderAddress})");
                    Console.WriteLine($"  To: {record.ReceiverName} ({record.ReceiverAddress})");
                    Console.WriteLine($"  Weights: {record.Weights}kg, Service ID: {record.ServiceID}\n");
                }
            }

            private void GenerateRevenueReport()
            {
                Console.WriteLine("\n--- Revenue Report ---");

                decimal totalRevenue = _courierService.GenerateRevenueReport();
                Console.WriteLine($"Total Revenue: {totalRevenue:C}");
            }

            private void AddCourierStaff()
            {
                Console.WriteLine("\n--- Add New Courier Staff ---");
                Employee employee = new Employee();

                Console.WriteLine("Enter EmployeeID: ");
                employee.EmployeeID = int.Parse(Console.ReadLine());

                Console.Write("Enter Staff Name: ");
                employee.Names = Console.ReadLine();

                Console.Write("Enter Email: ");
                employee.Email = Console.ReadLine();

                Console.Write("Enter Contact Number: ");
                employee.ContactNumber = Console.ReadLine();

                Console.Write("Enter Role: ");
                employee.Roles = Console.ReadLine();

                Console.Write("Enter Salary: ");
                employee.Salary = double.Parse(Console.ReadLine());

                Console.Write("Enter Location ID: ");
                employee.LocationID = int.Parse(Console.ReadLine());

                Console.Write("Enter Password: ");
                employee.Passwords = Console.ReadLine();

                int staffId = _adminService.AddCourierStaff(employee);
                Console.WriteLine($"New staff member added with ID: {staffId}");
            }
        }
    }
