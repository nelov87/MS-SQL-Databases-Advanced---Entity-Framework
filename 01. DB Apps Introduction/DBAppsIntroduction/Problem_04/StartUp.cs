using Problem_03;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Problem_04
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<string> minionInfo = Console.ReadLine().Split(" ").ToList();

            string town = minionInfo[3];
            string minionName = minionInfo[1];
            int age = int.Parse(minionInfo[2]);
            string townIdString = "";
            string villianIdString = "";
            string minionIdString = "";

            List<string> vilianInfo = Console.ReadLine().Split(" ").ToList();
            string vilianName = vilianInfo[1];



            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();
                try
                {
                    townIdString = CheckTownInDatabase(town, connection);

                    if (String.IsNullOrEmpty(townIdString))
                    {
                        insertTown(town, connection);
                        townIdString = CheckTownInDatabase(town, connection);
                        Console.WriteLine($"Town {town} was added to the database.");
                    }

                    villianIdString = CheckVillianInDatabase(vilianName, connection);

                    if (String.IsNullOrEmpty(villianIdString))
                    {
                        InsertVillian(vilianName, connection);
                        villianIdString = CheckVillianInDatabase(vilianName, connection);
                        Console.WriteLine($"Villain {vilianName} was added to the database.");

                    }

                    minionIdString = CheckMinionInDatabase(minionName, connection);
                    if (String.IsNullOrEmpty(minionIdString))
                    {
                        InsertMinion(town, minionName, age, connection);
                        minionIdString = CheckMinionInDatabase(minionName, connection);
                    }

                    MakeMinionSlaveToVillian(villianIdString, minionIdString, connection);
                    Console.WriteLine($"Successfully added {minionName} to be minion of {vilianName}");
                }
                catch(Exception e)
                {
                    Console.WriteLine("Nothing hapend");
                }

                connection.Close();
            }
        }

        private static void MakeMinionSlaveToVillian(string villianIdString, string minionIdString, SqlConnection connection)
        {
            string addMinionToVillian = "INSERT INTO MinionsVillains (MinionId, VillainId) VALUES (@villainId, @minionId)";
            using (SqlCommand command = new SqlCommand(addMinionToVillian, connection))
            {
                command.Parameters.AddWithValue("@villainId", int.Parse(villianIdString));
                command.Parameters.AddWithValue("@minionId", int.Parse(minionIdString));
                command.ExecuteNonQuery();
            }
        }

        private static void InsertMinion(string villianIdString, string minionName, int age, SqlConnection connection)
        {
            string insertMinionQuery = "INSERT INTO Minions (Name, Age, TownId) VALUES (@nam, @age, @townId)";
            using (SqlCommand command = new SqlCommand(insertMinionQuery, connection))
            {
                command.Parameters.AddWithValue("@nam", minionName);
                command.Parameters.AddWithValue("@age", age);
                command.Parameters.AddWithValue("@townId", int.Parse(villianIdString));
                command.ExecuteNonQuery();
            }
        }

        private static string CheckMinionInDatabase(string minionName, SqlConnection connection)
        {
            string minionIdString = "";
            string getMinionsQuery = "SELECT Id FROM Minions WHERE Name = @Name";
            using (SqlCommand command = new SqlCommand(getMinionsQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", minionName);
                minionIdString = Convert.ToString(command.ExecuteScalar());
            }

            return minionIdString;
        }

        private static void InsertVillian(string vilianName, SqlConnection connection)
        {
            string insertVillianQuery = "INSERT INTO Villains (Name, EvilnessFactorId)  VALUES (@villainName, 4)";
            using (SqlCommand command = new SqlCommand(insertVillianQuery, connection))
            {
                command.Parameters.AddWithValue("@villainName", vilianName);
                command.ExecuteNonQuery();
            }
        }

        private static string CheckVillianInDatabase(string vilianName, SqlConnection connection)
        {
            string villianIdString = "";
            string getVillianIDQuery = "SELECT Id FROM Villains WHERE Name = @Name";
            using (SqlCommand command = new SqlCommand(getVillianIDQuery, connection))
            {
                command.Parameters.AddWithValue("@Name", vilianName);
                villianIdString = Convert.ToString(command.ExecuteScalar());
            }

            return villianIdString;
        }

        private static void insertTown(string town, SqlConnection connection)
        {
            string insertTownQuery = "INSERT INTO Towns (Name) VALUES (@townName)";
            using (SqlCommand command = new SqlCommand(insertTownQuery, connection))
            {
                command.Parameters.AddWithValue("@townName", town);
                command.ExecuteNonQuery();
            }
        }

        private static string CheckTownInDatabase(string town, SqlConnection connection)
        {
            string townIdString = "";
            string getTownsQuery = "SELECT Id FROM Towns WHERE Name = @townName";

            using (SqlCommand command = new SqlCommand(getTownsQuery, connection))
            {
                command.Parameters.AddWithValue("@townName", town);
                townIdString = Convert.ToString(command.ExecuteScalar());


            }

            return townIdString;
        }
    }
}
