using Npgsql;

namespace APITest
{
    public class DatabaseService
    {
        private readonly IConfiguration Configuration;
        public DatabaseService(IConfiguration configuration)
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
                        command.CommandText = "SELECT * FROM size";

                        connection.Open();

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                test.Add(new Item(reader.GetString(0), reader.GetInt32(1)));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
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
