using Problem_03;
using System;
using System.Data.SqlClient;

namespace Problem_09
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                string execQuery = "EXEC usp_GetOlder @Id";
                using (SqlCommand command = new SqlCommand(execQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }

                string selectQuery = "SELECT * FROM Minions WHERE Id = @Id";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();
                    using (reader)
                    {
                        reader.Read();

                        Console.WriteLine($"{(string)reader["Name"]} - {(int)reader["Age"]} years old");
                    }

                }

                

                connection.Close();
            }
        }
    }
}
