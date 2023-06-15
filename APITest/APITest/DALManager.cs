using Npgsql;
using System.Diagnostics;

namespace API
{
    public class DALManager
    {
        private readonly IConfiguration Configuration;
        public DALManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<Item> GetItem()
        {
            List<Item> test = new List<Item>();

            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"]))
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM item";

                        connection.Open();

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                test.Add(new Item(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8)));
                            }
                        }
                    }
                }
                catch (NpgsqlException e)
                {
                    Debug.WriteLine("An NPG error occurred: " + e.Message);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return test;

        }
    }
}
