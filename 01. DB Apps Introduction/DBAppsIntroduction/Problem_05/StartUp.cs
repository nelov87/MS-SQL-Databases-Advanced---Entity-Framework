using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Problem_03;

namespace Problem_05
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string country = Console.ReadLine();
            int countOfAffectedTowns = 0;

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                int countryCode = GetCountryCode(connection, country);
                if (countryCode == 0)
                {
                    Console.WriteLine($"There is no country whith name {country} in Database");
                }
                else
                {
                    countOfAffectedTowns = UpdateTownsCaseing(connection, country);
                    if (countOfAffectedTowns == 0)
                    {
                        Console.WriteLine("No town names were affected.");
                    }
                    else
                    {
                        List<string> towns = new List<string>();
                        string getTownsQuery = "SELECT t.Name FROM Towns as t JOIN Countries AS c ON c.Id = t.CountryCode WHERE c.Name = @countryName";

                        using (SqlCommand command = new SqlCommand(getTownsQuery, connection))
                        {
                            command.Parameters.AddWithValue("@countryName", country);

                            SqlDataReader reader = command.ExecuteReader();

                            while (reader.Read())
                            {
                                towns.Add((string)reader["Name"]);
                            }
                            reader.Close();

                            Console.WriteLine($"{countOfAffectedTowns} town names were affected.");
                            Console.WriteLine($"[{string.Join(", ", towns)}]");
                        }
                    }
                }



                connection.Close();
            }
        }

        

        private static int UpdateTownsCaseing(SqlConnection connection, string cName)
        {
            string updateQuery = "UPDATE Towns SET Name = UPPER(Name) WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

            using (SqlCommand command = new SqlCommand(updateQuery, connection))
            {
                command.Parameters.AddWithValue("@countryName", cName);
                int affectedRows = (int)command.ExecuteNonQuery();

                return affectedRows;
            }
        }

        private static int GetCountryCode(SqlConnection connection, string country)
        {
            string getCountryesQyery = "SELECT Id FROM Countries WHERE Name = @countryName";

            using (SqlCommand command = new SqlCommand(getCountryesQyery, connection))
            {
                command.Parameters.AddWithValue("@countryName", country);
                
                string countString = Convert.ToString(command.ExecuteScalar());

                if (String.IsNullOrEmpty(countString))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(countString);
                }

            }
        }
    }
}
