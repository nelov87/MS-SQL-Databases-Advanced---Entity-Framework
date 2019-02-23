using Problem_03;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Problem_07
{
    class StartUp
    {
        static void Main(string[] args)
        {
            List<string> minions = new List<string>();
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                minions = GetMinions(connection);
                //int minionsCount = minions.Count;

                int count = 0;

                while (minions.Count >= 1)
                {
                    Console.WriteLine(minions[count]);
                    minions.RemoveAt(count);
                    if (minions.Count() > 0)
                    {
                        Console.WriteLine(minions[(minions.Count() - 1) - count]);
                        minions.RemoveAt((minions.Count() - 1) - count);
                    }
                }




                connection.Close();
            }
        }

        private static List<string> GetMinions(SqlConnection connection)
        {
            List<string> minions = new List<string>();
            string getCountMinionsQuery = "SELECT Name FROM Minions";
            using (SqlCommand command = new SqlCommand(getCountMinionsQuery, connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    minions.Add(reader["Name"].ToString());
                }
            }

            return minions;
        }
    }
}
