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
                        command.CommandText = "SELECT * FROM item order by id";

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

        public List<BItem> CallBasket()
        {
        List<BItem> Blist = new List<BItem>();

            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"]))
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM callbasket() order by id;";

                        connection.Open();

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Blist.Add(new BItem(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9)));
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
            return Blist;
        }

        public void AddToBasket(int id, int amount)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"]))
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "call addtobasket ($1, $2)";
                        command.Parameters.Add(new NpgsqlParameter() { Value = id });
                        command.Parameters.Add(new NpgsqlParameter() { Value = amount });

                        connection.Open();

                        command.ExecuteNonQuery();
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
        }

        public void DelFromBasket(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"]))
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "call delfrombasket ($1)";
                        command.Parameters.Add(new NpgsqlParameter() { Value = id });

                        connection.Open();

                        command.ExecuteNonQuery();
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
        }

        // Summary method - caluclate subtotal, moms, price & totalamount
        public List<BSum> Summary()
        {
            List<BSum> BSum = new List<BSum>(); //Basket Sum list
            int subtotal = 0;
            double moms = 0;
            int price = 0;
            int totalamount = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"])) //Connect to NpgsqlConnection
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM callbasket() order by id;"; //Select all from callbasket() - from the database

                        connection.Open();

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                subtotal += reader.GetInt32(1) * reader.GetInt32(8); //subtotal += reader index 1 * reader index 8
                                totalamount += reader.GetInt32(8); //totalamount += reader index 8
                            }
                        }
                    }
                    moms += subtotal * 0.2; //calculate moms
                    price += subtotal + 39; //calucalte price
                    BSum.Add(new BSum(subtotal, moms, price, totalamount)); //adds subtotal, moms, price & totalamount to BSum list
                }

                catch (NpgsqlException e) //Checks for Npgsql errors
                {
                    Debug.WriteLine("An NPG error occurred: " + e.Message);
                }
                catch (Exception ex) //Checks for all errors
                {
                    Debug.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return BSum; //Returns BSum list
        }
    }
}
