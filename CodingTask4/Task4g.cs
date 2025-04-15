using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CodingTask4
{
    class Task4g
    {
        static string connectionString = "Server=(localdb)\\mssqllocaldb; Database=CourierMgmtSys; Integrated Security=True;";

        public static List<string> FetchAddressesFromDatabase()
        {
            List<string> addresses = new List<string>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserAddress FROM Users";

                    SqlCommand command = new SqlCommand(query, connection);
                    connection.Open();

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            string address = reader.GetString(0).Trim();
                            addresses.Add(address);
                        }
                    }

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching addresses: " + ex.Message);
            }

            return addresses;
        }

        public static int GetSimilarityScore(string a, string b)
        {
            a = a.ToLower();
            b = b.ToLower();

            int matchCount = 0;
            string[] aWords = a.Split(' ');
            string[] bWords = b.Split(' ');

            foreach (string wordA in aWords)
            {
                foreach (string wordB in bWords)
                {
                    if (wordA == wordB)
                    {
                        matchCount++;
                    }
                }
            }
            return matchCount;
        }

        public static void FindSimilarAddresses(List<string> addresses)
        {
            int n = addresses.Count;
            Dictionary<string, bool> seen = new Dictionary<string, bool>();

            Console.WriteLine("\nSimilar Addresses Found:\n");

            bool found = false;

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    int score = GetSimilarityScore(addresses[i], addresses[j]);

                    if (score >= 3)
                    {
                        string key = addresses[i] + "|" + addresses[j];
                        if (!seen.ContainsKey(key))
                        {
                            Console.WriteLine($"- {addresses[i]}  ↔  {addresses[j]}");
                            seen[key] = true;
                            found = true;
                        }
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("No similar address pairs found.");
            }
        }

        static void Main()
        {
            Console.WriteLine("Fetching addresses from database...");

            List<string> addresses = FetchAddressesFromDatabase();

            if (addresses.Count == 0)
            {
                Console.WriteLine("No addresses found in the Users table.");
                return;
            }

            Console.WriteLine("\nFetched Addresses:");
            foreach (var addr in addresses)
            {
                Console.WriteLine("- " + addr);
            }

            FindSimilarAddresses(addresses);
        }
    }
}

