using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodingTask1
{
    class Task1d
    {
        static void AssignCourierToShipment(int courierId)
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Fetch all employee IDs
                    string employeeQuery = "SELECT EmployeeID FROM Employee";
                    SqlCommand cmdEmployees = new SqlCommand(employeeQuery, conn);
                    SqlDataReader empReader = cmdEmployees.ExecuteReader();

                    List<int> employeeIds = new List<int>();
                    while (empReader.Read())
                        employeeIds.Add(Convert.ToInt32(empReader["EmployeeID"]));
                    empReader.Close();

                    if (employeeIds.Count == 0)
                    {
                        Console.WriteLine("No employees available.");
                        return;
                    }

                    // Find employee with minimum load
                    int selectedEmployeeId = -1;
                    int minLoad = int.MaxValue;

                    foreach (int empId in employeeIds)
                    {
                        string countQuery = "SELECT COUNT(*) FROM Courier WHERE EmployeeID = @EmpID";
                        using (SqlCommand countCmd = new SqlCommand(countQuery, conn))
                        {
                            countCmd.Parameters.AddWithValue("@EmpID", empId);
                            int load = (int)countCmd.ExecuteScalar();

                            if (load < minLoad)
                            {
                                minLoad = load;
                                selectedEmployeeId = empId;
                            }
                        }
                    }

                    if (selectedEmployeeId == -1)
                    {
                        Console.WriteLine("Could not assign courier. No suitable employee found.");
                        return;
                    }

                    // Assign the courier
                    string updateQuery = "UPDATE Courier SET EmployeeID = @EmpID WHERE CourierID = @CourierID";
                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@EmpID", selectedEmployeeId);
                        updateCmd.Parameters.AddWithValue("@CourierID", courierId);

                        int rowsAffected = updateCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine($"Courier {courierId} assigned to employee {selectedEmployeeId} (Load: {minLoad}).");
                        else
                            Console.WriteLine("Assignment failed. Courier ID might be invalid.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Write("Enter CourierID to assign: ");
            if (int.TryParse(Console.ReadLine(), out int courierId))
            {
                AssignCourierToShipment(courierId);
            }
            else
            {
                Console.WriteLine("Invalid Courier ID.");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}