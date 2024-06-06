using Microsoft.AspNetCore.Mvc; // creating API controller
using MySql.Data.MySqlClient; // used to connecting MySql database
using ProductApi.Model; // is find where my propduct model is defined
using System.Data; // manipulate or optimize the data

namespace ProductApi.Controllers
{
    [Route("api/[controller]")] // the controller will be accessable at this path api/Product
    [ApiController] // it is used to serve all HTTP API Responces
    public class ProductController : ControllerBase
    {
        MySqlConnection conn= null; //conn is represents a connection of mysql database
        Mysqlconnect myconn = new Mysqlconnect(); // created a object of mysqlconnect class for established a conncetions

        // GET: api/<ProductController>
        [HttpGet] // method of a HTTP
        public List<Product> Get()
        {
            try
            {
               conn = new MySqlConnection(myconn.GetConnectionString()); // connection established

                MySqlCommand cmd = new MySqlCommand("InsertProduct", conn); // call the storedProcedure in the mysql database
                cmd.CommandType = CommandType.StoredProcedure; // represents the type of command: Stored Procedure, TableDirect, Text etc
                cmd.Parameters.AddWithValue("Product_ID", null);
                cmd.Parameters.AddWithValue("Name", null);
                cmd.Parameters.AddWithValue("Description", null);
                cmd.Parameters.AddWithValue("Price", null);
                cmd.Parameters.AddWithValue("Category", null);
                cmd.Parameters.AddWithValue("Query", 1);

                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(); // serves as a bridge between a Dataset and Database for retreiving and saving the data
                da.SelectCommand = cmd;
                DataSet ds = new DataSet(); // where we can store some multiple information
                da.Fill(ds);
                List<Product> productlist = new List<Product>(); // list of Product Object that will return by the Get method

                // using LINQ
                //Product objects from each row in the first table of the dataset, and adding them to productlist.
                productlist = (from DataRow row in ds.Tables[0].Rows
                               select new Product()
                               {
                                   Product_ID = Convert.ToInt32(row["Product_ID"].ToString()), // row[0]
                                   Name = row["Name"].ToString(), // row[1]
                                   Description = row["Description"].ToString(), //row[2]
                                   Price = Convert.ToDecimal(row["Price"].ToString()), // row[3]
                                   Category = row["Category"].ToString() // row[4]
                               }).ToList();

                return productlist;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public string Post(Product prd)

        {
            try
            {
                conn = new MySqlConnection(myconn.GetConnectionString());
                MySqlCommand cmd = new MySqlCommand("InsertProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Product_ID", prd.Product_ID);
                cmd.Parameters.AddWithValue("Name", prd.Name);
                cmd.Parameters.AddWithValue("Description", prd.Description);
                cmd.Parameters.AddWithValue("Price", prd.Price);
                cmd.Parameters.AddWithValue("Category", prd.Category);
                cmd.Parameters.AddWithValue("Query", 2);

                conn.Open();
                var recordsAffected = cmd.ExecuteNonQuery(); // Returns the number of records successfully affected
                conn.Close();

                if(recordsAffected > 0)
                    Console.WriteLine($"Records inserted: {recordsAffected}");

                return "Data inserted successfully";
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        [HttpPut]
        public string Put(Product prd)
        {
            try
            {

                conn = new MySqlConnection(myconn.GetConnectionString());
                MySqlCommand cmd = new MySqlCommand("InsertProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Product_ID", prd.Product_ID);
                cmd.Parameters.AddWithValue("Name", prd.Name);
                cmd.Parameters.AddWithValue("Description", prd.Description);
                cmd.Parameters.AddWithValue("Price", prd.Price);
                cmd.Parameters.AddWithValue("Category", prd.Category);
                cmd.Parameters.AddWithValue("Query", 3);

                conn.Open();
                var recordsAffected = cmd.ExecuteNonQuery(); // Returns the number of records successfully affected
                conn.Close();

                if (recordsAffected > 0)
                    Console.WriteLine($"Records Updated: {recordsAffected}");

                return "Data updated successfully";
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        // DELETE api/<ProductController>/5
        [HttpDelete]
        public string Delete(int id)
        {
            try
            {
               conn = new MySqlConnection(myconn.GetConnectionString());
                MySqlCommand cmd = new MySqlCommand("InsertProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Product_ID", id);
                cmd.Parameters.AddWithValue("Name", null);
                cmd.Parameters.AddWithValue("Description", null);
                cmd.Parameters.AddWithValue("Price", null);
                cmd.Parameters.AddWithValue("Category", null);
                cmd.Parameters.AddWithValue("Query", 4);

                conn.Open();
                var recordsAffected = cmd.ExecuteNonQuery(); // Returns the number of records successfully affected
                conn.Close();

                if (recordsAffected > 0)
                    Console.WriteLine($"Records Deleted: {recordsAffected}");

                return "Data deleted successfully";
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        // GET api/<ProductController>/5

        [HttpGet("id")]
        public List<Product> Get(int id)
        {
            try
            {
                conn = new MySqlConnection(myconn.GetConnectionString());
                MySqlCommand cmd = new MySqlCommand("InsertProduct", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("Product_ID", id);
                cmd.Parameters.AddWithValue("Name", null);
                cmd.Parameters.AddWithValue("Description", null);
                cmd.Parameters.AddWithValue("Price", null);
                cmd.Parameters.AddWithValue("Category", null);
                cmd.Parameters.AddWithValue("Query", 5);
                conn.Open();
                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);
                List<Product> productlist = new List<Product>();

                // using LINQ
                productlist = (from DataRow row in ds.Tables[0].Rows
                               select new Product()
                               {
                                   Product_ID = Convert.ToInt32(row["Product_ID"].ToString()),
                                   Name = row["Name"].ToString(),
                                   Description = row["Description"].ToString(),
                                   Price = Convert.ToDecimal(row["Price"].ToString()),
                                   Category = row["Category"].ToString()
                               }).ToList();

                return productlist;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

    }


}
