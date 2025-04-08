using System;
using System.Data.SqlClient;
using System.Globalization;

namespace CodingTask4
{
    class Task4c
    {
        private static readonly string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

        public static string CapitalizeEachWord(string input)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

        public static string FormatZipCode(string zip)
        {
            zip = zip.Trim();
            if (zip.Length == 6 && long.TryParse(zip, out _)) 
                return zip;
            else
                return "Invalid Zip Code";
        }

        public static string FormatAddress(string street, string city, string state, string zip, out string formattedZip)
        {
            formattedZip = FormatZipCode(zip);
            if (formattedZip == "Invalid Zip Code")
                return "Invalid Zip Code";

            string formattedStreet = CapitalizeEachWord(street.Trim());
            string formattedCity = CapitalizeEachWord(city.Trim());
            string formattedState = CapitalizeEachWord(state.Trim());

            return $"{formattedStreet}, {formattedCity}, {formattedState}";
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Enter the UserID to update the address:");
            int userId;
            while (!int.TryParse(Console.ReadLine(), out userId))
            {
                Console.Write("Invalid input. Please enter a numeric UserID: ");
            }

            Console.WriteLine("\nEnter Address Details Below:\n");

            Console.Write("Street: ");
            string street = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("State: ");
            string state = Console.ReadLine();

            Console.Write("Zip Code (6 digits): ");
            string zip = Console.ReadLine();

            string formattedZip;
            string formattedAddress = FormatAddress(street, city, state, zip, out formattedZip);

            if (formattedAddress == "Invalid Zip Code")
            {
                Console.WriteLine("\nError: Invalid Zip Code entered. Please use 6-digit zip codes.");
                return;
            }

            Console.WriteLine($"\nFormatted Address: {formattedAddress}");
            Console.WriteLine($"Formatted ZipCode: {formattedZip}");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string updateQuery = "UPDATE Users SET UserAddress = @UserAddress, ZipCode = @ZipCode WHERE UserID = @UserID";

                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserAddress", formattedAddress);
                        cmd.Parameters.AddWithValue("@ZipCode", formattedZip);
                        cmd.Parameters.AddWithValue("@UserID", userId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                            Console.WriteLine("\nUserAddress and ZipCode updated successfully in the database.");
                        else
                            Console.WriteLine("\nFailed to update. Please check if the UserID exists.");
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("\nDatabase Error: " + ex.Message);
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}