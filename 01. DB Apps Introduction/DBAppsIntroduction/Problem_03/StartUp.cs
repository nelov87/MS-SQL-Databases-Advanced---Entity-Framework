using System;
using System.Data.SqlClient;

namespace Problem_03
{
    class StartUp
    {
        static void Main(string[] args)
        {
            int id = int.Parse(Console.ReadLine());
            string name = "";
            using (SqlConnection connection = new SqlConnection(Configuration.ConnectionString))
            {
                connection.Open();

                name = GetName(connection, id);

                if (String.IsNullOrEmpty(name))
                {
                    Console.WriteLine($"No villain with ID {id} exists in the database.");
                    return;
                }
                else
                {
                    Console.WriteLine($"Villain: {name}");
                }

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                                   m.Name, 
                                                   m.Age
                                              FROM MinionsVillains AS mv
                                              JOIN Minions As m ON mv.MinionId = m.Id
                                             WHERE mv.VillainId = @Id
                                          ORDER BY m.Name";
                    command.Parameters.AddWithValue("@Id", id);

                    SqlDataReader reader = command.ExecuteReader();

                    int count = 1;

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("(no minions)");
                        reader.Close();
                        connection.Close();
                        return;
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            string minionName = reader["Name"].ToString();
                            string minionAge = reader["Age"].ToString();

                            Console.WriteLine($"{count++}. {minionName} {minionAge}");
                        }
                    }

                    
                }




                connection.Close();
            }
        }

        public static string GetName(SqlConnection connection, int id)
        {
            string name = "";
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection;
                command.CommandText = @"SELECT Name FROM Villains WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (!reader.HasRows)
                {
                    
                    reader.Close();
                    connection.Close();
                    
                }
                else
                {
                    while (reader.Read())
                    {
                        name = reader["Name"].ToString();
                    }
                }



                reader.Close();
            }

            return name;
        }
    }
}
