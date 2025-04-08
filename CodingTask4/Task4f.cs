using System;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;

namespace CodingTask4
{
    class Task4f
    {
        static Random rand = new Random();

        static string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

        static string GenerateSecurePassword(int length = 10)
        {
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string digits = "0123456789";
            const string special = "!@#$%^&*()-_=+";

            string allChars = upper + lower + digits + special;

            StringBuilder password = new StringBuilder();
            password.Append(upper[rand.Next(upper.Length)]);
            password.Append(lower[rand.Next(lower.Length)]);
            password.Append(digits[rand.Next(digits.Length)]);
            password.Append(special[rand.Next(special.Length)]);

            for (int i = 4; i < length; i++)
            {
                password.Append(allChars[rand.Next(allChars.Length)]);
            }

            char[] array = password.ToString().ToCharArray();
            for (int i = array.Length - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                (array[i], array[j]) = (array[j], array[i]);
            }

            return new string(array);
        }

        static void UpdatePasswords()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string getUsersQuery = "SELECT UserID FROM Users ORDER BY UserID";
                using (SqlCommand getUsersCmd = new SqlCommand(getUsersQuery, conn))
                using (SqlDataReader reader = getUsersCmd.ExecuteReader())
                {
                    List<int> userIds = new List<int>();

                    while (reader.Read())
                    {
                        userIds.Add(reader.GetInt32(0));
                    }

                    reader.Close();

                    foreach (int userId in userIds)
                    {
                        Console.WriteLine($"Updating password for UserID {userId}...");

                        string newPassword = GenerateSecurePassword();

                        string updateQuery = "UPDATE Users SET Passwords = @Passwords WHERE UserID = @UserID";
                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@Passwords", newPassword);
                            updateCmd.Parameters.AddWithValue("@UserID", userId);

                            int rowsAffected = updateCmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                Console.WriteLine($"Password updated for UserID {userId}: {newPassword}");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to update password for UserID {userId}.");
                            }
                        }
                    }
                }

                conn.Close();
            }
        }

        static void Main()
        {
            try
            {
                Console.WriteLine("Updating passwords for all users in the database (in order)...\n");
                UpdatePasswords();
                Console.WriteLine("\nAll operations completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
