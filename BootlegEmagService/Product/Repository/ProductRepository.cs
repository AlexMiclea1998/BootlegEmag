using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using BootlegEmagService.Product.Models;
using Microsoft.Extensions.Options;
using BootlegEmagService.ShoppingCart;

namespace BootlegEmagService.Product.Repository
{
    public class ProductRepository
    {
        private string _connectionString;

        public ProductRepository(IOptions<DatabaseConfiguration> configuration)
        {
            _connectionString = configuration.Value.ProductConnectionString;
        }

        public ProductModel CreateProduct(string name, string category, string price, string image)
        {
            using(var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                if (FindProduct(name))
                {
                    return null;
                }
                else
                {
                    var command = new SQLiteCommand(_connectionString);
                    command.CommandText = "INSERT INTO product(name, category, price, image) VALUES(@name, @category, @price, @image)";
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@category", category);
                    command.Parameters.AddWithValue("@price", price);
                    command.Parameters.AddWithValue("@image", image);
                    command.Prepare();
                    command.ExecuteNonQuery();
                    connection.Close();

                    return new ProductModel { Name = name, Category = category, Price = price, Image = image };
                }
            }         
        }

        public ProductModel DeleteProduct(ProductModel product)
        {
            using(var connection = new SQLiteConnection(_connectionString))
            {
                if (FindProduct(product.Name))
                {
                    connection.Open();
                    var command = new SQLiteCommand(_connectionString);
                    command.CommandText = "DELETE FROM product WHERE name = @name";
                    command.Parameters.AddWithValue("@name", product.Name);
                    command.Prepare();
                    command.ExecuteNonQuery();
                    connection.Close();
                    return product;
                }
            }         
            return null;
        }

        public ProductDTO UpdateProduct(ProductDTO product)
        {   
            using (var connection = new SQLiteConnection(_connectionString))
            {
                if (FindProduct(product.Name))
                {
                    connection.Open();
                    string stm2 = $"UPDATE product SET name= '{product.Name}', category='{product.Category}', price='{product.Price}', image='{product.Image}' WHERE id='{product.Id}'";
                    var check2 = new SQLiteCommand(stm2, connection);
                    check2.ExecuteReader();
                    connection.Close();
                    return product;
                }
                else
                {
                    return null;
                }
            }        
        }

        public List<ProductDTO> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string command = "SELECT * FROM product";
                var check = new SQLiteCommand(command, connection);
                SQLiteDataReader reader = check.ExecuteReader();
                List<ProductDTO> products = new List<ProductDTO>();
                while (reader.Read())
                {
                    string id = Convert.ToString(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    string category = Convert.ToString(reader["category"]);
                    string price = Convert.ToString(reader["price"]);
                    string image = Convert.ToString(reader["image"]);
                    products.Add(new ProductDTO { Id = id, Name = name, Category = category, Price = price, Image = image });
                }
                connection.Close();
                return products;
            }          
        }

        private bool FindProduct(string name)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(_connectionString);
                command.CommandText = "SELECT COUNT(UserId) FROM product WHERE name = @name";
                command.Parameters.AddWithValue("@name", name);
                string result = (string)command.ExecuteScalar();
                connection.Close();

                if (!string.IsNullOrEmpty(result))
                    return true;
                return false;
            }    
        }
    }
}