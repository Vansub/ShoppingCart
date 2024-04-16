using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace ShoppingCart.Models
{
    public class DbContext : IDbContext
    {
        MySqlConnection con { get; set; }

        public DbContext()
        {
            con = new MySqlConnection("server=localhost;uid=root;pwd=Password123!;database=shoppingcart");
        }

      
        public LoginStatus Login(string username, string passhash)
        {
            try
            {
                con.Open();
                string sql = @"SELECT UserID FROM User WHERE username = @username";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.Add(new MySqlParameter("@username", username));
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string userId = reader["UserId"].ToString();
                    reader.Close();
                    sql = @"SELECT 1 From User WHERE UserID = @userId and HashedPass = @passhash";
                    cmd = new MySqlCommand(sql, con);
                    cmd.Parameters.Add(new MySqlParameter("@userId", userId));
                    cmd.Parameters.Add(new MySqlParameter("@passhash", passhash));
                    MySqlDataReader reader2 = cmd.ExecuteReader();
                    if (reader2.Read())
                    {
                        return LoginStatus.Success;
                    }
                    else
                    {
                        return LoginStatus.Failed;
                    }
                }
                else
                {
                    return LoginStatus.Failed;
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return LoginStatus.Failed;
            }
            finally
            {
                con.Close();
            }
        }

        // Get all products
        public List<Software> GetAllProducts()
        {
            List<Software> products = new List<Software>();
            try
            {
                con.Open();
                string sql = @"SELECT softwareId, softwareName, description, price, imageUrl FROM Software";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Software product = new Software
                    {
                        softwareId = Convert.ToString(reader["softwareId"]),
                        softwareName = reader["softwareName"].ToString(),
                        description = reader["Description"].ToString(),
                        price = Convert.ToDouble(reader["Price"]),
                       imageUrl = reader["ImageUrl"].ToString()
                    };
                    products.Add(product);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return products;
        }

        // Search products
        public List<Software> SearchProducts(string searchTerm)
        {
            List<Software> products = new List<Software>();
            try
            {
                con.Open();
                string sql = @"SELECT ProductId, Name, Description, Price, ImageUrl FROM Products WHERE Name LIKE @searchTerm";
                MySqlCommand cmd = new MySqlCommand(sql, con);
                cmd.Parameters.Add(new MySqlParameter("@searchTerm", "%" + searchTerm + "%"));
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Software product = new Software
                    {
                        softwareId = Convert.ToString(reader["softwareId"]),
                        softwareName = reader["softwareName"].ToString(),
                        description = reader["Description"].ToString(),
                        price = Convert.ToDouble(reader["Price"]),
                        imageUrl = reader["ImageUrl"].ToString()
                    };
                    products.Add(product);
                }

                reader.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return products;
        }

       
        public void AddToCart(int userId, int productId)
        {
            
        }
    }
}
