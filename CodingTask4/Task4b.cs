using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CodingTask4
{
    class Task4b
    {
        static void Main()
        {
            Console.WriteLine("Connecting to database...");

            string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Connection successful!\n");

                    EnsureDummyDataExists(connection);

                    string query = "SELECT Names, UserAddress, ContactNumber FROM Users";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No user data found in the database.");
                        }

                        while (reader.Read())
                        {
                            string name = reader["Names"].ToString();
                            string address = reader["UserAddress"].ToString();
                            string phone = reader["ContactNumber"].ToString();

                            string formattedPhone = FormatPhoneNumber(phone);

                            Console.WriteLine($"Name: {name}");
                            Console.WriteLine($"Address: {address}");
                            Console.WriteLine($"Phone: {formattedPhone}");

                            PrintValidationResult("Name", ValidateCustomerData(name, "name"));
                            PrintValidationResult("Address", ValidateCustomerData(address, "address"));
                            PrintValidationResult("Phone", ValidateCustomerData(phone, "phone"));

                            Console.WriteLine("---------------------------------------");
                        }
                    }

                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            Console.WriteLine("\nValidation Completed. Press any key to exit....");
            Console.ReadKey();
        }

        static void EnsureDummyDataExists(SqlConnection connection)
        {
            string checkQuery = "SELECT COUNT(*) FROM Users";
            using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
            {
                int count = (int)checkCommand.ExecuteScalar();
                if (count == 0)
                {
                    Console.WriteLine("Inserting dummy data into Users table...\n");

                    string insertQuery = @"
                        INSERT INTO Users (Names, UserAddress, ContactNumber) VALUES 
                        ('Priyanka Sharma', '123 Main Street', '9876543210'),
                        ('Amit Kumar', 'B-56, Sector 12, New Delhi', '9123456789'),
                        ('John Doe', '45 Park Avenue', '9988776655')";

                    using (SqlCommand insertCommand = new SqlCommand(insertQuery, connection))
                    {
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        static string FormatPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length == 10 && Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                return $"{phoneNumber.Substring(0, 3)}-{phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6, 4)}";
            }
            return phoneNumber;
        }

        static bool ValidateCustomerData(string data, string detail)
        {
            switch (detail.ToLower())
            {
                case "name":
                    return Regex.IsMatch(data, @"^[A-Z][a-zA-Z]*( [A-Z][a-zA-Z]*)*$");
                case "address":
                    return Regex.IsMatch(data, @"^[\w\s,./#-]+$");
                case "phone":
                    return Regex.IsMatch(data, @"^\d{10}$");
                default:
                    return false;
            }
        }

        static void PrintValidationResult(string label, bool isValid)
        {
            Console.ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"{label} Valid: {isValid}");
            Console.ResetColor();
        }
    }
}