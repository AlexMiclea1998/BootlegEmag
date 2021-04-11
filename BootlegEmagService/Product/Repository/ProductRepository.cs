using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using BootlegEmagService.Product.Models;

namespace BootlegEmagService.Product.Repository
{
    public class ProductRepository
    {

        private SQLiteConnection _connection;

        public ProductRepository()
        {
            OpenConnection();
        }

        private void OpenConnection()
        {
            if (_connection == null)
            {
                var dbPath = ConfigurationManager.ConnectionStrings["ProductDBConnection"].ConnectionString;
                _connection = new SQLiteConnection(dbPath);
            }
        }

        public ProductModel CreateProduct(string name, string category, string price, string image)
        {
            _connection.Open();
            if (FindProduct(name))
            {
                return null;
            }
            else
            {
                var command = new SQLiteCommand(_connection);
                command.CommandText = "INSERT INTO product(name, category, price, image) VALUES(@name, @category, @price, @image)";
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@category", category);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@image", image);
                command.Prepare();
                command.ExecuteNonQuery();
                _connection.Close();

                return new ProductModel { Name = name, Category = category, Price = price, Image = image };
            }
        }

        public ProductModel DeleteProduct(ProductModel product)
        {
            if (FindProduct(product.Name))
            {
                _connection.Open();
                var command = new SQLiteCommand(_connection); 
                command.CommandText = "DELETE FROM product WHERE name = @name";
                command.Parameters.AddWithValue("@name", product.Name);
                command.Prepare();
                command.ExecuteNonQuery();
                _connection.Close();
                return product;
            }
            return null;
        }

        public ProductDTO UpdateProduct(ProductDTO product)
        {       
            if (FindProduct(product.Name))
            {
                _connection.Open();
                string stm2 = $"UPDATE product SET name= '{product.Name}', category='{product.Category}', price='{product.Price}', image='{product.Image}' WHERE id='{product.Id}'";
                var check2 = new SQLiteCommand(stm2, _connection);
                check2.ExecuteReader();
                _connection.Close();
                return product;
            }
            else
            {
                return null;
            }
        }

        public List<ProductDTO> GetAll()
        {
            _connection.Open();
            string command = "SELECT * FROM product";
            var check = new SQLiteCommand(command, _connection);
            SQLiteDataReader reader = check.ExecuteReader();
            List<ProductDTO> products = new List<ProductDTO>();
            while (reader.Read())
            {
                string id = Convert.ToString(reader["id"]);
                string name = Convert.ToString(reader["name"]);
                string category = Convert.ToString(reader["category"]);
                string price = Convert.ToString(reader["price"]);
                string image = Convert.ToString(reader["image"]);
                products.Add(new ProductDTO { Id = id, Name = name, Category = category, Price = price, Image = image}); 
            }
            return products;
        }

        private bool FindProduct(string name)
        {
            _connection.Open();
            var command = new SQLiteCommand(_connection);
            command.CommandText = "SELECT COUNT(UserId) FROM product WHERE name = @name";
            command.Parameters.AddWithValue("@name", name);
            string result = (string)command.ExecuteScalar();

            if (!string.IsNullOrEmpty(result))
                return true;
            return false;
        }
    }
}