using Problem_03;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Problem_08
{
    class StartUp
    {
        static void Main(string[] args)
        {
            List<int> idToEdit = Console.ReadLine().Split(" ").Select(int.Parse).ToList();
            Dictionary<string, int> minions = new Dictionary<string, int>();

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                EditMinions(connection, idToEdit);
                minions = GetMinions(connection);

                foreach (var minion in minions)
                {
                    Console.WriteLine($"{minion.Key} {minion.Value}");
                }


                connection.Close();
            }
        }


        private static Dictionary<string, int> GetMinions(SqlConnection connection)
        {
            Dictionary<string, int> minions = new Dictionary<string, int>();
            string getCountMinionsQuery = "SELECT Name, Age FROM Minions";
            using (SqlCommand command = new SqlCommand(getCountMinionsQuery, connection))
            {
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    minions.Add(reader["Name"].ToString(), (int)reader["Age"]);
                }
            }

            return minions;
        }

        private static void EditMinions(SqlConnection connection, List<int> idToEdit)
        {
            string editQuery = " UPDATE Minions SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1 WHERE Id = @Id";

            for (int i = 0; i < idToEdit.Count(); i++)
            {
                using (SqlCommand command = new SqlCommand(editQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", idToEdit[i]);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
