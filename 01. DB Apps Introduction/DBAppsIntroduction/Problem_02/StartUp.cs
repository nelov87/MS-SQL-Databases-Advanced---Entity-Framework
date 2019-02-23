using System;
using System.Data.SqlClient;

namespace Problem_02
{
    class StartUp
    {
        static void Main(string[] args)
        {
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.CommandText = (@"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                               FROM Villains AS v 
                                               JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                           GROUP BY v.Id, v.Name 
                                             HAVING COUNT(mv.VillainId) > 3 
                                           ORDER BY COUNT(mv.VillainId)");

                    command.Connection = connection;

                    SqlDataReader reader = command.ExecuteReader();
                   
                      while (reader.Read())
                      {
                          Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}");
                      }
                   

                }




                    connection.Close();
            }
        }
    }
}
