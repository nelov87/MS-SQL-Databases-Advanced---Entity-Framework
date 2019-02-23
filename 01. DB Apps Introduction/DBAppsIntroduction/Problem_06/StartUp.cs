using Problem_03;
using System;
using System.Data.SqlClient;

namespace Problem_06
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int villianId = int.Parse(Console.ReadLine());
            int minionsCount = 0;
            string villianName = "";

            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                villianName = CheckVilian(connection, villianId);
                if (String.IsNullOrEmpty(villianName))
                {
                    Console.WriteLine("No such villain was found.");
                    return;
                }
                else
                {
                    minionsCount = ReleseMinions(connection, villianId);
                }

                connection.Close();
            }


            Console.WriteLine($"{villianName} was deleted.");
            Console.WriteLine($"{minionsCount} minions were released.");
        }

        private static int ReleseMinions(SqlConnection connection, int villianId)
        {
            int countAffectedRows = 0;
            
            string releseMinionsQuery = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
            using (SqlCommand command = new SqlCommand(releseMinionsQuery, connection))
            {
                command.Parameters.AddWithValue("@villainId", villianId);
                countAffectedRows = command.ExecuteNonQuery();
            }

            string deleteVillainQuery = "DELETE FROM Villains WHERE Id = @villainId";
            using (SqlCommand command = new SqlCommand(deleteVillainQuery, connection))
            {
                command.Parameters.AddWithValue("@villainId", villianId);
                command.ExecuteNonQuery();
            }

                return countAffectedRows;

        }

        private static string CheckVilian(SqlConnection connection, int villianId)
        {
            string villanName = "";
            string chseckVillianQuery = "SELECT Name FROM Villains WHERE Id = @villianId ";
            using (SqlCommand command = new SqlCommand(chseckVillianQuery, connection))
            {
                command.Parameters.AddWithValue("@villianId", villianId);
                villanName = (string)command.ExecuteScalar();
            }
            return villanName;
        }
    }
}
