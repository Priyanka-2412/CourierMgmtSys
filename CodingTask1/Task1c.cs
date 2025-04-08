using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodingTask1
{
    class Task1c
    {
        static string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CourierMgmtSys;Integrated Security=True;";

        static void Main(string[] args)
        {
            int choice;

            do
            {
                Console.WriteLine("\n1. User Sign up\t2. User Login\t3. Register Employee\t4. Login Employee\t5. Exit");
                Console.Write("Enter your choice: ");
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        RegisterUser();
                        break;
                    case 2:
                        LoginUser();
                        break;
                    case 3:
                        RegisterEmployee();
                        break;
                    case 4:
                        LoginEmployee();
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }

            } while (choice != 5);
        }

        static void RegisterUser()
        {
            Console.WriteLine("--------------New user registration--------------");
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.Write("Enter your email: ");
            string userEmail = Console.ReadLine();
            Console.Write("Enter your contact number: ");
            string contactNumber = Console.ReadLine();

            string password, confirmPassword;
            do
            {
                Console.Write("Enter your password: ");
                password = Console.ReadLine();
                Console.Write("Re-enter your password: ");
                confirmPassword = Console.ReadLine();

                if (password != confirmPassword)
                    Console.WriteLine("Passwords do not match. Try again.");
            }
            while (password != confirmPassword);

            string hashedPassword = HashPassword(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Users (Names, Email, ContactNumber, Passwords) VALUES (@name, @Email, @ContactNumber, @password)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", userName);
                    cmd.Parameters.AddWithValue("@Email", userEmail);
                    cmd.Parameters.AddWithValue("@ContactNumber", contactNumber);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (result > 0)
                        Console.WriteLine($"Successfully registered, welcome {userName}!");
                    else
                        Console.WriteLine("Registration failed. Try again.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void LoginUser()
        {
            Console.WriteLine("--------------User login--------------");
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            string hashedPassword = HashPassword(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"SELECT Passwords FROM Users WHERE Names = @name";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", userName);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != null)
                    {
                        string storedPassword = result.ToString();

                        if (storedPassword == password || storedPassword == hashedPassword)
                        {
                            Console.WriteLine($"Login successful, welcome {userName}!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid username or password.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void RegisterEmployee()
        {
            Console.WriteLine("--------------New Employee Registration--------------");
            Console.Write("Enter employee name: ");
            string empName = Console.ReadLine();
            Console.Write("Enter employee email: ");
            string empEmail = Console.ReadLine();
            Console.Write("Enter employee contact number: ");
            string empContact = Console.ReadLine();

            string password, confirmPassword;
            do
            {
                Console.Write("Enter password: ");
                password = Console.ReadLine();
                Console.Write("Re-enter password: ");
                confirmPassword = Console.ReadLine();

                if (password != confirmPassword)
                    Console.WriteLine("Passwords do not match. Try again.");
            }
            while (password != confirmPassword);

            string hashedPassword = HashPassword(password);
            string role = "Employee";
            decimal salary = 30000;
            int locationID = 1;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Employee 
                                    (Names, Email, ContactNumber, Passwords, Roles, Salary, LocationID) 
                                     VALUES 
                                    (@name, @Email, @ContactNumber, @password, @role, @salary, @locationID)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", empName);
                    cmd.Parameters.AddWithValue("@Email", empEmail);
                    cmd.Parameters.AddWithValue("@ContactNumber", empContact);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    cmd.Parameters.AddWithValue("@role", role);
                    cmd.Parameters.AddWithValue("@salary", salary);
                    cmd.Parameters.AddWithValue("@locationID", locationID);

                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (result > 0)
                        Console.WriteLine($"Employee {empName} registered successfully!");
                    else
                        Console.WriteLine("Employee registration failed.");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static void LoginEmployee()
        {
            Console.WriteLine("--------------Employee Login--------------");
            Console.Write("Enter employee name: ");
            string empName = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            string hashedPassword = HashPassword(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Passwords FROM Employee WHERE Names = @name";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@name", empName);

                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    conn.Close();

                    if (result != null)
                    {
                        string storedPassword = result.ToString();

                        if (storedPassword == hashedPassword || storedPassword == password)
                        {
                            Console.WriteLine($"Login successful, welcome {empName}!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid name or password.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid name or password.");
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}