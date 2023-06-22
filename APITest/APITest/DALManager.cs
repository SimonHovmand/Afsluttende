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

        //GetItem method - calls GetItem sql command
        public List<Item> GetItem()
        {
            List<Item> ItemList = new List<Item>();

            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"])) //Connecting to Npgsql using the ConnectionString
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM item order by id"; //The SQL command "SELECT * FROM item order by id"

                        connection.Open();

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) //While reader.read is true - add a new item of Item to the ItemList with the values.
                            {
                                ItemList.Add(new Item(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8)));
                            }
                        }
                    }
                }
                catch (NpgsqlException e) //Cathes Npgsql errors
                {
                    Debug.WriteLine("An NPG error occurred: " + e.Message);
                }
                catch (Exception ex) //Cathes all errors
                {
                    Debug.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return ItemList; //Returns ItemList list
        }

        //CallBasket method - calls CallBasket sql command
        public List<BItem> CallBasket()
        {
        List<BItem> Blist = new List<BItem>(); //Create a list with the class BItem 

            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"])) //Connecting to Npgsql using the ConnectionString
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM callbasket() order by id;"; //The SQL command "SELECT * FROM callbasket() order by id"

                        connection.Open();

                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read()) //While reader.read is true - add a new item of BItem to the Blist with the values.
                            {
                                Blist.Add(new BItem(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetInt32(8), reader.GetInt32(9)));
                            }
                        }
                    }
                }

                catch (NpgsqlException e) //Cathes Npgsql errors
                {
                    Debug.WriteLine("An NPG error occurred: " + e.Message);
                }
                catch (Exception ex) //Cathes all errors
                {
                    Debug.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return Blist; //Returns Blist list
        }

        //AddToBasket method - calls AddToBasket sql command with the item id and item amount
        public void AddToBasket(int id, int amount)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"])) //Connecting to Npgsql using the ConnectionString
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "call addtobasket ($1, $2)"; //The SQL command "call delfrombasket ($1)"
                        command.Parameters.Add(new NpgsqlParameter() { Value = id }); //Sets the first parameter valuer to id
                        command.Parameters.Add(new NpgsqlParameter() { Value = amount }); //Sets the second parameter valuer to amount

                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException e) //Cathes Npgsql errors
                {
                    Debug.WriteLine("An NPG error occurred: " + e.Message);
                }
                catch (Exception ex) //Cathes all errors
                {
                    Debug.WriteLine("An error occurred: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        // DeleteFromBasket method - calls delfrembasket sql command with the item id
        public void DelFromBasket(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"])) //Connecting to Npgsql using the ConnectionString
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "call delfrombasket ($1)"; //The SQL command "call delfrombasket ($1)"
                        command.Parameters.Add(new NpgsqlParameter() { Value = id }); //Sets the first parameter valuer to id

                        connection.Open();

                        command.ExecuteNonQuery();
                    }
                }
                catch (NpgsqlException e) //Cathes Npgsql errors
                {
                    Debug.WriteLine("An NPG error occurred: " + e.Message);
                }
                catch (Exception ex) //Cathes all errors
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

            using (NpgsqlConnection connection = new NpgsqlConnection(Configuration["ConnectionString"])) //Connecting to Npgsql using the ConnectionString
            {
                try
                {
                    using (NpgsqlCommand command = new NpgsqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = "SELECT * FROM callbasket() order by id;"; //The SQL command "Select all from callbasket()

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

                catch (NpgsqlException e) //Cathes Npgsql errors
                {
                    Debug.WriteLine("An NPG error occurred: " + e.Message);
                }
                catch (Exception ex) //Cathes all errors
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
